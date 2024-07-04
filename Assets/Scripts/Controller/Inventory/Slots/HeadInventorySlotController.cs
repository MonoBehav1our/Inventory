using Model.Inventory;
using View.Inventory;
using UnityEngine;

namespace Controller.Inventory
{
    public class HeadInventorySlotController : InventorySlotController<HeaddressItem>
    {
        private InventorySlot<HeaddressItem> _inventorySlot;
        private EquipmentInventorySlotView _inventorySlotView;

        public HeadInventorySlotController(InventorySlot<HeaddressItem> inventorySlot, EquipmentInventorySlotView inventorySlotView) 
            : base(inventorySlot, inventorySlotView)
        {
            _inventorySlot = inventorySlot;
            _inventorySlotView = inventorySlotView;

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
