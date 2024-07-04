using UnityEngine;

namespace Model.Inventory
{
    [CreateAssetMenu(menuName = "Item configs/new HeaddressItemConfig")]
    public class HeaddressItemInfo : ItemInfo
    {
        [field: SerializeField] public int ArmorValue { get; private set; }

        public override Item CreateThisItem() => new HeaddressItem(this);    
    }
}
