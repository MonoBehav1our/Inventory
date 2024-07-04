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

        //for loading
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


        //for create a new data
        public InventoryGridData(InventoryGrid inventoryGrid, bool fill)
        {
            HeadItemData = new InventorySlotData(ConfigManager.NullID, 0);
            OutwearItemData = new InventorySlotData(ConfigManager.NullID, 0);
            MainSlotsData = new List<InventorySlotData>();

            for (int i = 0; i < inventoryGrid.MainSlotsGrid.GetLength(0); i++)
            {
                for (int j = 0; j < inventoryGrid.MainSlotsGrid.GetLength(1); j++)
                {
                    MainSlotsData.Add(new InventorySlotData(ConfigManager.NullID, 0));
                }
            }

            if (fill)
            {
                MainSlotsData[0] = new InventorySlotData("PISTOL_AMMO", 50);
                MainSlotsData[1] = new InventorySlotData("RIFLE_AMMO", 100);
                MainSlotsData[2] = new InventorySlotData("HEALTH_KIT", 6);
                MainSlotsData[3] = new InventorySlotData("OUTWEAR_2", 1);
                MainSlotsData[4] = new InventorySlotData("OUTWEAR_1", 1);
                MainSlotsData[5] = new InventorySlotData("HEADDRESS_2", 1);
                MainSlotsData[6] = new InventorySlotData("HEADDRESS_1", 1);
            }
        }
    }
}
