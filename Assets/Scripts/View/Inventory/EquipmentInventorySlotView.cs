using UnityEngine;
using System;
using TMPro;

namespace View.Inventory
{
    public class EquipmentInventorySlotView : InventorySlotView 
    {
        [SerializeField] private TextMeshProUGUI _armorText;

        public int Armor
        {
            get => Convert.ToInt32(_armorText.text);
            set => _armorText.text = value.ToString();
        }
    }
}
