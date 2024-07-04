using UnityEngine;

namespace Model.Inventory
{
    [CreateAssetMenu(menuName = "Item configs/ new AmmoItemConfig")]
    public class AmmoItemInfo : ItemInfo
    {
        [field: SerializeField] public AmmoType AmmoType;

        public override Item CreateThisItem() => new AmmoItem(this);
    }
}
