using Controller.Inventory;

namespace Model.Inventory
{
    public class MedKitItem : Item
    {
        public int HealthValue { get; private set; }

        public MedKitItem(MedKitItemInfo so) : base(so)
        {
            this.HealthValue = so.HealthValue;
        }
    }
}
