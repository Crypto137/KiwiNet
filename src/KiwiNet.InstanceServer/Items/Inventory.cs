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
        NumTypes,
    }

    public class Inventory
    {
        private readonly Entry[] _slots;
        private readonly Dictionary<uint, Entry> _entries = new();
        private readonly List<IInventorySubscriber> _subscribers = new();

        public InventoryCategory Category { get; }
        public InventoryType Type { get; }
        public bool Flag0 { get; }
        public bool Flag1 { get; }
        public bool Flag2 { get; }
        public int Width { get; }
        public int Height { get; }

        public Inventory(InventoryCategory category, InventoryType type, int width, int height, bool flag0, bool flag1, bool flag2)
        {
            Category = category;
            Type = type;
            Width = width;
            Height = height;
            Flag0 = flag0;
            Flag1 = flag1;
            Flag2 = flag2;

            _slots = new Entry[width * height];
        }

        public override string ToString()
        {
            return Enum.GetName(Type);
        }

        public class Entry
        {
            public Item Item { get; set; }
            public int X1 { get; set; }
            public int Y1 { get; set; }
            public int X2 { get; set; }
            public int Y2 { get; set; }
            public uint Key { get; set; }

            public override string ToString()
            {
                return $"[{X1},{Y1}] {Item}";
            }
        }
    }
}
