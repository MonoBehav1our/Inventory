using Model.Inventory.Saves;
using UnityEngine;
using System;

namespace Model.Inventory
{
    public class InventoryGrid
    {
        public InventorySlot<HeaddressItem> HeadSlot { get; private set; }
        public InventorySlot<OutwearItem> OutwearSlot { get; private set; }

        public readonly InventorySlot<Item>[,] MainSlotsGrid = new InventorySlot<Item>[5, 6];

        public InventoryGrid(InventoryGridData data)
        {
            InitInventorySlots();

            HeadSlot.SetItem(data.HeadItemData);
            OutwearSlot.SetItem(data.OutwearItemData);

            for (int i = 0; i < MainSlotsGrid.GetLength(0); i++)
            {
                for (int j = 0; j < MainSlotsGrid.GetLength(1); j++)
                {
                    MainSlotsGrid[i, j].SetItem(data.MainSlotsData[j + (i * MainSlotsGrid.GetLength(1))]);
                }
            }
        }

        public InventoryGrid() => InitInventorySlots();

        public void AddItem(string id, int slotNumber, int amount = 1)
        {

            if (id == ConfigManager.NullID)
                throw new InvalidOperationException("Item ID for add can't be a \"NULL\"");
            if (amount < 1)
                throw new InvalidOperationException("Amount for add should be more than 0");

            int i = slotNumber / MainSlotsGrid.GetLength(1); 
            int j = slotNumber % MainSlotsGrid.GetLength(1);

            var slot = MainSlotsGrid[i, j];
            Item item = ConfigManager.Instance.CreateItemByID(id);

            if (slot.IsEmpty)
            {
                if (amount <= item.MaxAmount)
                {
                    slot.SetItem(item, amount);
                    WriteDebugInfo();
                }
                else
                {
                    int remaning = amount - item.MaxAmount;
                    slot.SetItem(item, item.MaxAmount);
                    WriteDebugInfo();
                    SearchSlotAndAddItem(id, remaning);
                }                    
            }
                
            else
            {
                if (slot.ItemID == item.ID)
                {
                    if (amount + slot.Amount <= item.MaxAmount)
                    {
                        slot.AddAmount(amount);
                        WriteDebugInfo();
                    }
                    else
                    {
                        int remaning = amount + slot.Amount - item.MaxAmount;
                        slot.AddAmount(amount - remaning);
                        WriteDebugInfo();
                        SearchSlotAndAddItem(id, remaning);
                    }
                }
                else SearchSlotAndAddItem(id, amount);
            }
            InventorySaver.Save(new InventoryGridData(this));
        }

        public void SearchSlotAndAddItem(string id, int amount = 1)
        {
            if (id == ConfigManager.NullID)
                throw new InvalidOperationException("Item ID for add can't be a \"NULL\"");
            if (amount < 1)
                throw new InvalidOperationException("Amount for add should be more than 0");

            for (int i = 0; i < MainSlotsGrid.GetLength(0); i++)
            {
                for (int j = 0; j < MainSlotsGrid.GetLength(1); j++)
                {
                    if (MainSlotsGrid[i, j].ItemID == id && MainSlotsGrid[i, j].Amount < MainSlotsGrid[i, j].Item.MaxAmount)
                    {
                        AddItem(id, j + (i * MainSlotsGrid.GetLength(1)), amount);
                        return;
                    }
                }
            }

            for (int i = 0; i < MainSlotsGrid.GetLength(0); i++)
            {
                for (int j = 0; j < MainSlotsGrid.GetLength(1); j++)
                {
                    if (MainSlotsGrid[i, j].IsEmpty)
                    {
                        AddItem(id, j + (i * MainSlotsGrid.GetLength(1)), amount);
                        return;
                    }
                }
            }
        }

        public void RemoveItem(string id, int slotNumber, int removeAmount = 1)
        {
            if (id == ConfigManager.NullID)
                throw new InvalidOperationException("Item ID for remove can't be a \"NULL\"");
            if (removeAmount < 1) 
                throw new InvalidOperationException("Amount for remove should be more than 0");

            //var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
            //var type = assembly.GetType("UnityEditor.LogEntries");
            //var method = type.GetMethod("Clear");
            //method.Invoke(new object(), null);

            int i = slotNumber / MainSlotsGrid.GetLength(1);
            int j = slotNumber % MainSlotsGrid.GetLength(1);

            var slot = MainSlotsGrid[i, j];

            if (!slot.IsEmpty)
            {
                if (slot.ItemID == id)
                {
                    if (removeAmount < slot.Amount)
                    {
                        slot.Amount -= removeAmount;
                        WriteDebugInfo();
                    }
                    else
                    {
                        int remaning = removeAmount - slot.Amount;
                        slot.Clear();
                        WriteDebugInfo();

                        if (remaning > 0) SearchAndRemoveItem(id, remaning);
                    }
                }
            }
            else
            {
                SearchAndRemoveItem(id, removeAmount);
            }
            InventorySaver.Save(new InventoryGridData(this));
        }

        public void SearchAndRemoveItem(string id, int removeAmount = 1)
        {
            if (id == ConfigManager.NullID)
                throw new InvalidOperationException("Item ID for add can't be a \"NULL\"");
            if (removeAmount < 1)
                throw new InvalidOperationException("Amount for add should be more than 0");

            for (int i = 0; i < MainSlotsGrid.GetLength(0); i++)
            {
                for (int j = 0; j < MainSlotsGrid.GetLength(1); j++)
                {
                    if (!MainSlotsGrid[i, j].IsEmpty && MainSlotsGrid[i, j].ItemID == id)
                    {
                        RemoveItem(id, j + (i * MainSlotsGrid.GetLength(1)), removeAmount);
                        return;
                    }
                }
            }
        }

        public void SwapItems(int firstSlotNumber, int secondSlotNumber)
        {
            int i1 = firstSlotNumber / MainSlotsGrid.GetLength(1);
            int j1 = firstSlotNumber % MainSlotsGrid.GetLength(1);

            int i2 = secondSlotNumber / MainSlotsGrid.GetLength(1);
            int j2 = secondSlotNumber % MainSlotsGrid.GetLength(1);

            InventorySlotData firstData = MainSlotsGrid[i1, j1].CreateSlotData();
            InventorySlotData secondData = MainSlotsGrid[i2, j2].CreateSlotData();

            MainSlotsGrid[i1, j1].SetItem(secondData);
            MainSlotsGrid[i2, j2].SetItem(firstData);

            InventorySaver.Save(new InventoryGridData(this));
        }

        public void Equipe(InventorySlot<Item> equipmentSlot)
        {
            foreach (InventorySlot<Item> slot in MainSlotsGrid) 
            {
                if (slot == equipmentSlot)
                {
                    if (equipmentSlot.Item.GetType() == typeof(HeaddressItem))
                    {
                        var firstData = slot.CreateSlotData();
                        var secondData = HeadSlot.CreateSlotData();

                        slot.SetItem(secondData);
                        HeadSlot.SetItem(firstData);
                    }
                    else if (equipmentSlot.Item.GetType() == typeof(OutwearItem))
                    {
                        var firstData = slot.CreateSlotData();
                        var secondData = OutwearSlot.CreateSlotData();

                        slot.SetItem(secondData);
                        OutwearSlot.SetItem(firstData);
                    }
                    else
                        throw new Exception("Tring to equipe a not-equipable slot");
                }
            }

            InventorySaver.Save(new InventoryGridData(this));
        }

        public bool TryUseBullet(AmmoType type)
        {
            for (int i = 0; i < MainSlotsGrid.GetLength(0); i++)
            {
                for (int j = 0; j < MainSlotsGrid.GetLength(1); j++)
                {
                    if (!MainSlotsGrid[i, j].IsEmpty && MainSlotsGrid[i, j].Item.GetType() == typeof(AmmoItem))
                    {
                        AmmoItem item = (AmmoItem)MainSlotsGrid[i, j].Item;
                        if (item.AmmoType == type)
                        {
                            MainSlotsGrid[i,j].Amount -= 1;
                            return true;
                        }
                            
                    }
                }
            }
            return false;
        }

        public void Clear()
        {
            HeadSlot.Clear();
            OutwearSlot.Clear();

            for (int i = 0; i < MainSlotsGrid.GetLength(0); i++)
            {
                for (int j = 0; j < MainSlotsGrid.GetLength(1); j++)
                {
                    MainSlotsGrid[i, j].Clear();
                }
            }
            Debug.Log("Clear");
            InventorySaver.Save(new InventoryGridData(this));
        }

        public void WriteDebugInfo()
        {
            
            //Debug.Log("DEBUG");
            //Debug.Log(HeadSlot.ItemID + ": " + HeadSlot.Amount);
            //Debug.Log(OutwearSlot.ItemID + ": " + OutwearSlot.Amount);

            //for (int i = 0; i < MainSlotsGrid.GetLength(0); i++)
            //{
            //    for (int j = 0; j < MainSlotsGrid.GetLength(1); j++)
            //    {
            //        Debug.Log(MainSlotsGrid[i, j].ItemID + ": " + MainSlotsGrid[i, j].Amount);
            //    }
            //}
        }

        private void InitInventorySlots()
        {
            HeadSlot = new InventorySlot<HeaddressItem>();
            OutwearSlot = new InventorySlot<OutwearItem>();

            for (int i = 0; i < MainSlotsGrid.GetLength(0); i++)
            {
                for (int j = 0; j < MainSlotsGrid.GetLength(1); j++)
                {
                    MainSlotsGrid[i, j] = new InventorySlot<Item>();
                }
            }
        }
    }
}
