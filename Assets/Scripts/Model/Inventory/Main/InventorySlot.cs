using Model.Inventory.Saves;
using UnityEngine;
using System;

namespace Model.Inventory
{
    public class InventorySlot<TItem> where TItem : Item
    {
        public event Action OnChanged;

        //properties | fields
        public TItem Item { get; protected set; }
        public Sprite Sprite => Item == null ? null : Item.Sprite;
        public int Amount
        {
            get => _amount; set 
            {
                _amount = value;
                if (_amount <= 0)
                    Clear();

                OnChanged?.Invoke();
            }
        }

        protected int _amount;

        public bool IsEmpty => Item == null;
        public string ItemID => GetID();


        //contructors
        public InventorySlot(InventorySlotData data)
        {
            Item = (TItem)ConfigManager.Instance.CreateItemByID(data.ID);
            Amount = data.Amount;

            OnChanged?.Invoke();
        }
        public InventorySlot()
        {
            Item = null; 
            Amount = 0;

            OnChanged?.Invoke();
        }

        //public
        public InventorySlotData CreateSlotData() => new InventorySlotData(Item != null ? Item.ID : ConfigManager.NullID, Amount);

        public void AddAmount(int amount) => Amount += amount;

        public void SetItem(InventorySlotData data) 
        {
            Item = (TItem)ConfigManager.Instance.CreateItemByID(data.ID);
            Amount = data.Amount;

            if (Item == null) Amount = 0;

            OnChanged?.Invoke();
        }
        public virtual void SetItem(TItem item, int amount = 1)
        {
            Item = item;
            Amount = Mathf.Clamp(amount, 1, Item.MaxAmount);

            if (Item == null) Amount = 0;

            OnChanged?.Invoke();
        }

        public void Clear()
        {
            Item = null;
            _amount = 0;

            OnChanged?.Invoke();
        }

        //private 
        private string GetID()
        {
            if (Item != null)
                return Item.ID;
            else
                return ConfigManager.NullID;
        }
    }
}
