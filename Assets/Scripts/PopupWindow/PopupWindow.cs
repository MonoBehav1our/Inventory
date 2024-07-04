using Model.Inventory;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

namespace Controller.Inventory
{
    public class PopupWindow : MonoBehaviour 
    {
        private Action<InventorySlot<Item>> OnClick;

        [SerializeField] private GameObject _windowGO;

        [Space]
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _someValueText;
        [SerializeField] private TextMeshProUGUI _heightText;
        [SerializeField] private TextMeshProUGUI _buttonText;
        [SerializeField] private Image _image;
        [SerializeField] private Image _someImage;
        [SerializeField] private Sprite[] _someSprites;

        public string Name
        {
            get => _nameText.text;
            set => _nameText.text = value;
        }

        public string SomeValue
        {
            get => _someValueText.text;
            set => _someValueText.text = value;
        }

        public float Height
        {
            get => Convert.ToSingle(_heightText.text);
            set => _heightText.text = value.ToString();
        }

        public string ButtonText
        {
            get => _buttonText.text;
            set => _buttonText.text = value;
        }

        public Sprite Sprite
        {
            get => _image.sprite;
            set => _image.sprite = value;
        }

        private InventorySlot<Item> _cashedSlot;
        private InventoryGrid _inventory;

        public void Init(InventoryGrid inventory)
        {
            _inventory = inventory;
        }

        public void Open(InventorySlot<Item> slot)
        {
            if (slot.IsEmpty) return;

            _cashedSlot = slot;
            _windowGO.SetActive(true);

            if (slot.Item.GetType() == typeof(AmmoItem))
            {
                OnClick = FillSlot;
                AmmoItem ammoItem = (AmmoItem)slot.Item;
                switch (ammoItem.AmmoType)
                {
                    case AmmoType.RifleAmmo5_45x39:
                        _someImage.sprite = _someSprites[0];
                        break;
                    case AmmoType.PistolAmmo9x18:
                        _someImage.sprite = _someSprites[1];
                        break;
                }
                ButtonText = "Купить";
            }
            else if (slot.Item.GetType() == typeof(HeaddressItem) || slot.Item.GetType() == typeof(OutwearItem))
            {
                OnClick = EquipeItem;
                IEquipmentItem equipItem = (IEquipmentItem)slot.Item;
                _someImage.sprite = _someSprites[2];
                SomeValue = "+" + equipItem.ArmorValue.ToString();
                ButtonText = "Экипировать";
            }
            else if (slot.Item.GetType() == typeof(MedKitItem))
            {
                OnClick = UseHill;
                MedKitItem medKitItem = (MedKitItem)slot.Item;
                _someImage.sprite = _someSprites[3];
                SomeValue = "+" + medKitItem.HealthValue.ToString();
                ButtonText = "Лечить";
            }
            else
                throw new NotImplementedException($"There is no action for type: \"{slot.Item.GetType()}\"");

            Name = slot.Item.Name;
            Height = slot.Item.Height;
            Sprite = slot.Item.Sprite;
        }

        public void Click() => OnClick?.Invoke(_cashedSlot);
        public void Close() => _windowGO.SetActive(false);

        public void Delete()
        {
            if (_cashedSlot != null)
                _cashedSlot.Clear();

            Close();
        }

        private void FillSlot(InventorySlot<Item> slot)
        {
            if (slot == null) return;

            slot.Amount = slot.Item.MaxAmount;
            Close();
        }

        private void EquipeItem(InventorySlot<Item> slot)
        {
            if (slot == null) return;

            _inventory.Equipe(slot);
            Close();
        }

        private void UseHill(InventorySlot<Item> slot)
        {
            if (slot == null) return;

            slot.Amount -= 1;
            Close();
        }        
    }
}

