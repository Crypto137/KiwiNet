using KiwiNet.Core.Extensions;
using KiwiNet.Core.Logging;
using KiwiNet.Core.Math;
using KiwiNet.Core.Network;
using KiwiNet.InstanceServer.Items.Components;
using KiwiNet.InstanceServer.Items.Components.Templates;

namespace KiwiNet.InstanceServer.Items
{
    public enum InventoryCategory
    {
        General,
        BodyArmour,
        Weapon,
        Offhand,
        Helm,
        Amulet,
        Ring,
        Gloves,
        Boots,
        Belt,
        Flask,
        Cursor,
        Stash,
        AltarSmall,
        AltarMedium,
        AltarLarge,
        VendorYourNetWorth,
        VendorForSale,
        NumCategories,
    }

    public enum InventoryType
    {
        MainInventory1,
        BodyArmour1,
        Weapon1,
        Offhand1,
        Helm1,
        Amulet1,
        Ring1,
        Ring2,
        Gloves1,
        Boots1,
        Belt1,
        Flask1,
        Cursor1,
        Stash1,
        Stash2,
        Stash3,
        Stash4,
        Stash5,
        Stash6,
        Stash7,
        Stash8,
        Stash9,
        Stash10,
        AltarSmall1,
        AltarSmall2,
        AltarMedium1,
        AltarMedium2,
        AltarMedium3,
        AltarLarge1,
        AltarLarge2,
        AltarLarge3,
        VendorSellRemote1,
        VendorSellLocal1,
        VendorForSale1,
        VendorYourNetWorth1,
        TradeLocal1,
        TradeRemote1,
        InAnotherItem,
        Invalid,
        NumTypes = Invalid,
    }

    public class Inventory
    {
        public const uint InvalidEntryId = 0;

        private static readonly Logger Logger = LogManager.CreateLogger();

        private uint _currentEntryId = 0;
        private readonly List<Entry> _slots = new();
        private readonly Dictionary<uint, Entry> _entries = new();
        private readonly List<IInventorySubscriber> _subscribers = new();
        private readonly List<uint> _addedEntries = new();
        private readonly List<uint> _removedEntries = new();

        public InventoryCategory Category { get; }
        public InventoryType Type { get; }
        public bool OneItemOnly { get; }
        public bool IsEquipment { get; }
        public bool IsStash { get; }
        public int Width { get; }
        public int Height { get; }

        public Inventory(InventoryCategory category, InventoryType type, int width, int height, bool oneItemOnly, bool isEquipment, bool isStash)
        {
            Category = category;
            Type = type;
            Width = width;
            Height = height;
            OneItemOnly = oneItemOnly;
            IsEquipment = isEquipment;
            IsStash = isStash;

            _slots.Fill(null, width * height);
        }

        public override string ToString()
        {
            return Enum.GetName(Type);
        }

        public void Destroy()
        {
            foreach (Entry entry in _entries.Values)
                entry.Item.Destroy();

            _removedEntries.Clear();
            _addedEntries.Clear();
            _subscribers.Clear();
            _entries.Clear();
            _slots.Clear();
        }

        public void Serialize(NetworkConnection connection)
        {
            connection.Write(_entries.Count);
            foreach (Entry entry in _entries.Values)
                entry.Serialize(connection);
        }

        public void SerializeUpdate(NetworkConnection connection)
        {
            connection.Write(_removedEntries.Count);
            foreach (uint removedItem in _removedEntries)
                connection.Write(removedItem);

            connection.Write(_addedEntries.Count);
            foreach (uint addedItem in _addedEntries)
            {
                Entry entry = _entries[addedItem];
                entry.Serialize(connection);
            }
        }

        public void ResetUpdate()
        {
            _addedEntries.Clear();
            _removedEntries.Clear();
        }
    
        public bool IsDirty()
        {
            return _addedEntries.Count != 0 || _removedEntries.Count != 0;
        }

        public bool IsBlocked(in RectInt rect, out Entry outExistingEntry)
        {
            outExistingEntry = null;

            if (OneItemOnly && (rect.X1 != 0 || rect.Y1 != 0))
                return true;

            if (rect.X1 < 0 || rect.Y1 < 0 || rect.X2 > Width || rect.Y2 > Height)
                return true;

            Entry existingEntry = null;

            for (int y = rect.Y1; y < rect.Y2; y++)
            {
                for (int x = rect.X1; x < rect.X2; x++)
                {
                    Entry entry = _slots[y * Width + x];
                    if (entry != null)
                    {
                        // When the rect is blocked by two different entries, neither of them is returned in the out argument.
                        if (existingEntry != null && existingEntry != entry)
                            return true;
                        
                        existingEntry = entry;
                    }
                }
            }

            if (existingEntry != null)
            {
                outExistingEntry = existingEntry;
                return true;
            }

            return false;
        }

        public uint AddItem(Item item, int x, int y)
        {
            Base itemBase = item.GetComponent<Base>();

            if (OneItemOnly && (x != 0 || y != 0))
            {
                Logger.Warn("Failing to add item because this inventory can only add one item and it has to be in the top left");
                return InvalidEntryId;
            }

            BaseTemplate baseTemplate = itemBase.Template;
            
            int x2 = x + baseTemplate.Width;
            int y2 = y + baseTemplate.Height;
            
            RectInt rect = new(x, y, x2, y2);

            if (x < 0 || y < 0 || x2 > Width || y2 > Height)
            {
                Logger.Warn("Failing to add item because it is not in bounds");
                return InvalidEntryId;
            }

            if (IsBlocked(rect, out _))
            {
                Logger.Warn($"Failing to add item because the location ({x}, {y}) is blocked. Item we are trying to place is: {baseTemplate.Name}");
                return InvalidEntryId;
            }

            uint entryId = GetNextEntryId();
            
            Entry entry = new()
            {
                Item = item,
                Rect = rect,
                Id = entryId,
            };

            AddEntry(entry);
            _addedEntries.Add(entryId);
            return entryId;
        }

        public Item RemoveItem(uint id)
        {
            if (_entries.TryGetValue(id, out Entry entry) == false)
                return null;

            Item item = entry.Item;
            RemoveEntry(entry);

            int addedIndex = _addedEntries.IndexOf(id);
            if (addedIndex == -1)
                _removedEntries.Add(id);
            else
                _addedEntries.SwapRemove(addedIndex);

            return item;
        }

        public Item GetItem(uint id)
        {
            if (_entries.TryGetValue(id, out Entry entry) == false)
                return null;

            return entry.Item;
        }

        public void Subscribe(IInventorySubscriber subscriber)
        {
            _subscribers.Add(subscriber);
        }

        public void Unsubscribe(IInventorySubscriber subscriber)
        {
            _subscribers.Remove(subscriber);
        }

        private uint GetNextEntryId()
        {
            uint id = _currentEntryId;

            do
            {
                id++;
            }
            while (_entries.ContainsKey(id) || id == InvalidEntryId);

            _currentEntryId = id;
            return id;
        }

        private void AddEntry(Entry entry)
        {
            RectInt rect = entry.Rect;

            for (int y = rect.Y1; y < rect.Y2; y++)
            {
                for (int x = rect.X1; x < rect.X2; x++)
                    _slots[y * Width + x] = entry;
            }

            _entries.Add(entry.Id, entry);

            foreach (IInventorySubscriber subscriber in _subscribers)
                subscriber.OnItemAdded(this, entry.Item, new(rect.X1, rect.Y1));
        }

        private void RemoveEntry(Entry entry)
        {
            RectInt rect = entry.Rect;

            for (int y = rect.Y1; y < rect.Y2; y++)
            {
                for (int x = rect.X1; x < rect.X2; x++)
                    _slots[y * Width + x] = null;
            }

            _entries.Remove(entry.Id);

            foreach (IInventorySubscriber subscriber in _subscribers)
                subscriber.OnItemRemoved(this, entry.Item);
        }

        public class Entry
        {
            public Item Item;
            public RectInt Rect;
            public uint Id;

            public override string ToString()
            {
                return $"[{Rect.X1}, {Rect.Y1}] {Item} ({Id})";
            }

            public void Serialize(NetworkConnection connection)
            {
                connection.Write(Id);
                connection.Write((byte)Rect.X1);
                connection.Write((byte)Rect.Y1);
                connection.Write(Item);
            }
        }
    }
}
