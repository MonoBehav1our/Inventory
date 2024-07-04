using System.Collections.Generic;
using System;

namespace Model.Inventory.Saves
{
    [Serializable]
    public struct InventoryGridData
    {
        public InventorySlotData HeadItemData;
        public InventorySlotData OutwearItemData;
        public List<InventorySlotData> MainSlotsData;

        public InventoryGridData(InventoryGrid inventoryGrid)
        {
            HeadItemData = new InventorySlotData(inventoryGrid.HeadSlot.ItemID, inventoryGrid.HeadSlot.Amount);
            OutwearItemData = new InventorySlotData(inventoryGrid.OutwearSlot.ItemID, inventoryGrid.OutwearSlot.Amount);
            MainSlotsData = new List<InventorySlotData>();

            for (int i = 0; i < inventoryGrid.MainSlotsGrid.GetLength(0); i++)
            {
                for (int j = 0; j < inventoryGrid.MainSlotsGrid.GetLength(1); j++)
                {
                    MainSlotsData.Add(new InventorySlotData(inventoryGrid.MainSlotsGrid[i, j].ItemID, inventoryGrid.MainSlotsGrid[i, j].Amount));
                }
            }
        }
    }
}
