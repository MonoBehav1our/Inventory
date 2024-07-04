using System;

namespace Model.Inventory.Saves
{
    [Serializable]
    public struct InventorySlotData
    {
        public string ID;
        public int Amount;

        public InventorySlotData(string id = ConfigManager.NullID, int amount = 0)
        {
            ID = id;
            Amount = amount;
        }
    }
}
