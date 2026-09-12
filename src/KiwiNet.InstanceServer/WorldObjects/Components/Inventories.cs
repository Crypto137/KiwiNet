using KiwiNet.Core.Math;
using KiwiNet.Core.Network;
using KiwiNet.InstanceServer.Items;
using KiwiNet.InstanceServer.Objects;
using KiwiNet.InstanceServer.WorldObjects.Components.Templates;

namespace KiwiNet.InstanceServer.WorldObjects.Components
{
    public sealed class Inventories : WorldComponent, IInventorySubscriber
    {
        private readonly Inventory[] _inventories = new Inventory[(int)InventoryType.NumTypes];

        private Stats _stats;
        private Life _life;

        public override void Initialize(ComponentTemplate template, ObjectBase owner)
        {
            base.Initialize(template, owner);

            InventoriesTemplate inventoriesTemplate = (InventoriesTemplate)template;

            _stats = owner.GetComponent<Stats>(inventoriesTemplate.StatsIndex);
            _life = owner.GetComponent<Life>(inventoriesTemplate.LifeIndex);

            //                                                            Category                              Type                                W   H   OneItemOnly  IsEquipment  IsStash
            _inventories[(int)InventoryType.MainInventory1]         = new(InventoryCategory.General,            InventoryType.MainInventory1,       12, 5,  false,       false,       false   );
            _inventories[(int)InventoryType.VendorSellRemote1]      = new(InventoryCategory.General,            InventoryType.VendorSellRemote1,    12, 5,  false,       false,       false   );
            _inventories[(int)InventoryType.VendorSellLocal1]       = new(InventoryCategory.General,            InventoryType.VendorSellLocal1,     12, 5,  false,       false,       false   );
            _inventories[(int)InventoryType.TradeLocal1]            = new(InventoryCategory.General,            InventoryType.TradeLocal1,          12, 5,  false,       false,       false   );
            _inventories[(int)InventoryType.TradeRemote1]           = new(InventoryCategory.General,            InventoryType.TradeRemote1,         12, 5,  false,       false,       false   );
            _inventories[(int)InventoryType.VendorForSale1]         = new(InventoryCategory.VendorForSale,      InventoryType.VendorForSale1,       12, 10, false,       false,       false   );
            _inventories[(int)InventoryType.VendorYourNetWorth1]    = new(InventoryCategory.VendorYourNetWorth, InventoryType.VendorYourNetWorth1,  10, 2,  false,       false,       false   );
            _inventories[(int)InventoryType.Stash1]                 = new(InventoryCategory.Stash,              InventoryType.Stash1,               12, 12, false,       false,       true    );
            _inventories[(int)InventoryType.Stash2]                 = new(InventoryCategory.Stash,              InventoryType.Stash2,               12, 12, false,       false,       true    );
            _inventories[(int)InventoryType.Stash3]                 = new(InventoryCategory.Stash,              InventoryType.Stash3,               12, 12, false,       false,       true    );
            _inventories[(int)InventoryType.Stash4]                 = new(InventoryCategory.Stash,              InventoryType.Stash4,               12, 12, false,       false,       true    );
            _inventories[(int)InventoryType.Stash5]                 = new(InventoryCategory.Stash,              InventoryType.Stash5,               12, 12, false,       false,       true    );
            _inventories[(int)InventoryType.Stash6]                 = new(InventoryCategory.Stash,              InventoryType.Stash6,               12, 12, false,       false,       true    );
            _inventories[(int)InventoryType.Stash7]                 = new(InventoryCategory.Stash,              InventoryType.Stash7,               12, 12, false,       false,       true    );
            _inventories[(int)InventoryType.Stash8]                 = new(InventoryCategory.Stash,              InventoryType.Stash8,               12, 12, false,       false,       true    );
            _inventories[(int)InventoryType.Stash9]                 = new(InventoryCategory.Stash,              InventoryType.Stash9,               12, 12, false,       false,       true    );
            _inventories[(int)InventoryType.Stash10]                = new(InventoryCategory.Stash,              InventoryType.Stash10,              12, 12, false,       false,       true    );
            _inventories[(int)InventoryType.AltarSmall1]            = new(InventoryCategory.AltarSmall,         InventoryType.AltarSmall1,          1,  1,  true,        true,        false   );
            _inventories[(int)InventoryType.AltarSmall2]            = new(InventoryCategory.AltarSmall,         InventoryType.AltarSmall2,          1,  1,  true,        true,        false   );
            _inventories[(int)InventoryType.AltarMedium1]           = new(InventoryCategory.AltarMedium,        InventoryType.AltarMedium1,         1,  2,  true,        true,        false   );
            _inventories[(int)InventoryType.AltarMedium2]           = new(InventoryCategory.AltarMedium,        InventoryType.AltarMedium2,         1,  2,  true,        true,        false   );
            _inventories[(int)InventoryType.AltarMedium3]           = new(InventoryCategory.AltarMedium,        InventoryType.AltarMedium3,         1,  2,  true,        true,        false   );
            _inventories[(int)InventoryType.AltarLarge1]            = new(InventoryCategory.AltarLarge,         InventoryType.AltarLarge1,          2,  2,  true,        true,        false   );
            _inventories[(int)InventoryType.AltarLarge2]            = new(InventoryCategory.AltarLarge,         InventoryType.AltarLarge2,          2,  2,  true,        true,        false   );
            _inventories[(int)InventoryType.AltarLarge3]            = new(InventoryCategory.AltarLarge,         InventoryType.AltarLarge3,          2,  2,  true,        true,        false   );
            _inventories[(int)InventoryType.BodyArmour1]            = new(InventoryCategory.BodyArmour,         InventoryType.BodyArmour1,          2,  3,  true,        true,        false   );
            _inventories[(int)InventoryType.Weapon1]                = new(InventoryCategory.Weapon,             InventoryType.Weapon1,              2,  4,  true,        true,        false   );
            _inventories[(int)InventoryType.Offhand1]               = new(InventoryCategory.Offhand,            InventoryType.Offhand1,             2,  4,  true,        true,        false   );
            _inventories[(int)InventoryType.Helm1]                  = new(InventoryCategory.Helm,               InventoryType.Helm1,                2,  2,  true,        true,        false   );
            _inventories[(int)InventoryType.Amulet1]                = new(InventoryCategory.Amulet,             InventoryType.Amulet1,              1,  1,  true,        true,        false   );
            _inventories[(int)InventoryType.Ring1]                  = new(InventoryCategory.Ring,               InventoryType.Ring1,                1,  1,  true,        true,        false   );
            _inventories[(int)InventoryType.Ring2]                  = new(InventoryCategory.Ring,               InventoryType.Ring2,                1,  1,  true,        true,        false   );
            _inventories[(int)InventoryType.Gloves1]                = new(InventoryCategory.Gloves,             InventoryType.Gloves1,              2,  2,  true,        true,        false   );
            _inventories[(int)InventoryType.Boots1]                 = new(InventoryCategory.Boots,              InventoryType.Boots1,               2,  2,  true,        true,        false   );
            _inventories[(int)InventoryType.Belt1]                  = new(InventoryCategory.Belt,               InventoryType.Belt1,                2,  1,  true,        true,        false   );
            _inventories[(int)InventoryType.Flask1]                 = new(InventoryCategory.Flask,              InventoryType.Flask1,               5,  2,  false,       true,        false   );
            _inventories[(int)InventoryType.Cursor1]                = new(InventoryCategory.Cursor,             InventoryType.Cursor1,              2,  4,  true,        false,       false   );

            foreach (Inventory inventory in _inventories)
            {
                if (inventory != null && inventory.IsEquipment)
                    inventory.Subscribe(this);
            }
        }

        public override void Destroy()
        {
            foreach (Inventory inventory in _inventories)
                inventory?.Destroy();

            base.Destroy();
        }

        public override void Serialize(NetworkConnection connection)
        {
            // Inventory order/count is the same between the client and the server, so it is not serialized.
            foreach (Inventory inventory in _inventories)
                inventory?.Serialize(connection);
        }

        public override void ResetUpdate()
        {
            foreach (Inventory inventory in _inventories)
                inventory?.ResetUpdate();
        }

        #region IInventorySubscriber

        public void OnItemAdded(Inventory inventory, Item item, Vector2Int position)
        {
            // TODO: update stats?
        }

        public void OnItemRemoved(Inventory inventory, Item item)
        {
            // TODO: update stats?
        }

        #endregion
    }
}
