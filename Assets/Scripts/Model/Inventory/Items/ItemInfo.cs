using UnityEngine;

namespace Model.Inventory
{
    
    public abstract class ItemInfo : ScriptableObject
    {
        [field: SerializeField] public string ID { get; private set; }

        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }        

        [field: SerializeField] public int MaxAmount { get; private set; }
        [field: SerializeField] public float Height { get; private set; }

        public abstract Item CreateThisItem();
    }
}
