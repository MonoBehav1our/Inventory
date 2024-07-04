using UnityEngine;

namespace Model.Inventory
{
    [CreateAssetMenu(menuName = "Item configs/ new OutwearItemConfig")]
    public class OutwearItemInfo : ItemInfo
    {

        [field: SerializeField] public int ArmorValue { get; private set; }

        public override Item CreateThisItem() => new OutwearItem(this);
    }
}
