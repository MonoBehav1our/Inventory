using Model.Inventory;
using View.Inventory;

namespace Controller.Inventory
{
    public class InventoryGridController
    {
        public InventoryGridController(InventoryGrid inventoryGrid, InventoryGridView inventoryView)
        {
            new HeadInventorySlotController(inventoryGrid.HeadSlot, inventoryView.HeadSlotView);
            new OutwearInventorySlotController(inventoryGrid.OutwearSlot, inventoryView.OutwearSlotView);

            for (int i = 0; i < inventoryGrid.MainSlotsGrid.GetLength(0); i++)
            {
                for (int j = 0; j < inventoryGrid.MainSlotsGrid.GetLength(1); j++)
                {
                    new InventorySlotController<Item>(inventoryGrid.MainSlotsGrid[i, j], inventoryView.MainSlotsViews[j + (i * inventoryGrid.MainSlotsGrid.GetLength(1))]);
                }
            }
        }
    }
}