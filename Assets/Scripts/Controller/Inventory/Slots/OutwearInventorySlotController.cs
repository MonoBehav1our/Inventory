using Model.Inventory;
using View.Inventory;

namespace Controller.Inventory
{
    public class OutwearInventorySlotController : InventorySlotController<OutwearItem>
    {
        private InventorySlot<OutwearItem> _inventorySlot;
        private EquipmentInventorySlotView _inventorySlotView;

        public OutwearInventorySlotController(InventorySlot<OutwearItem> inventorySlot, EquipmentInventorySlotView inventorySlotView) 
            : base(inventorySlot, inventorySlotView) 
        {
            _inventorySlot = inventorySlot;
            _inventorySlotView = inventorySlotView;

            inventorySlotView.Armor = 0;
            UpdateView();
        }

        protected override void UpdateView()
        {
            if (_inventorySlotView != null)
                _inventorySlotView.Sprite = _inventorySlot.Sprite;

            if (_inventorySlot != null && _inventorySlotView != null)
            {
                if (_inventorySlot.Item != null)
                    _inventorySlotView.Armor = _inventorySlot.Item.ArmorValue;
                else
                    _inventorySlotView.Armor = 0;
            }
        }
    }
}
