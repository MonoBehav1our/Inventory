using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace View.Inventory
{
    public class InventorySlotView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _amountText;
        [SerializeField] private Image _itemImage;

        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;

                if (_amount <= 1)
                    _amountText.text = string.Empty;
                else 
                    _amountText.text = value.ToString(); 
            }
        }

        public Sprite Sprite 
        { 
            get => _sprite;
            set
            {
                _sprite = value;
                _itemImage.sprite = value;

                if (_sprite == null)
                    _itemImage.enabled = false;
                else
                    _itemImage.enabled = true;
            }
        }

        private int _amount;
        private Sprite _sprite;
    }
}
