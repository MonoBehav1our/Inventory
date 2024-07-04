using View.Inventory;
using Model.Inventory;
using UnityEngine;

namespace Controller.Inventory
{
    public class InventorySlotController<TItem> where TItem : Item
    {
        private InventorySlot<TItem> _inventorySlot;
        private InventorySlotView _inventorySlotView;

        public InventorySlotController(InventorySlot<TItem> inventorySlot, InventorySlotView inventorySlotView) 
        {
            _inventorySlot = inventorySlot;
            _inventorySlotView = inventorySlotView;

            UpdateView();
            _inventorySlot.OnChanged += UpdateView; 
        }

        protected virtual void UpdateView()
        {
            _inventorySlotView.Amount = _inventorySlot.Amount;
            _inventorySlotView.Sprite = _inventorySlot.Sprite;
        }

        ~InventorySlotController()
        {
            _inventorySlot.OnChanged -= UpdateView;
        }
    }
}
