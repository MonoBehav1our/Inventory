using UnityEngine;

namespace Model.Inventory
{
    [CreateAssetMenu(menuName = "Item configs/new HealthKitItemConfig")]
    public class MedKitItemInfo : ItemInfo
    {
        [field: SerializeField] public int HealthValue {  get; private set; }

        public override Item CreateThisItem() => new MedKitItem(this);
    }
}