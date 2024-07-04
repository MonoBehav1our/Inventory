using Controller.Inventory;
using UnityEngine;

namespace Model.Inventory
{
    public abstract class Item
    {
        public string ID { get; protected set; }
        public Sprite Sprite { get; protected set; }
        public string Name { get; protected set; }

        public int MaxAmount { get; protected set; }
        public float Height { get; protected set; }

        public Item(ItemInfo so)
        {
            ID = so.ID;
            Sprite = so.Sprite;
            Name = so.Name;

            MaxAmount = so.MaxAmount;
            Height = so.Height;
        }
    }
}