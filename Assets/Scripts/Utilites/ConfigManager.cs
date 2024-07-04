using Model.Inventory;
using UnityEngine;
using System;

public class ConfigManager : MonoBehaviour
{
    public static ConfigManager Instance { get; private set; }
    public const string NullID = "NULL";

    [SerializeField] private string _testID;
    [SerializeField] private ItemInfo[] _configs;

    public static readonly string[] _IDs = { "RIFLE_AMMO", "PISTOL_AMMO", "OUTWEAR_1", "OUTWEAR_2", "HEADDRESS_1", "HEADDRESS_2", "HEALTH_KIT" };

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

    }

    public Item CreateItemByID(string itemID)
    {
        if (itemID == NullID) return null;

        Item item = null;
        foreach (ItemInfo itemInfo in _configs)
        {
            if (itemID == itemInfo.ID)
            {
                if (item != null)
                    throw new InvalidOperationException($"2 or more configs have the \"{itemID}\" ID");

                item = itemInfo.CreateThisItem();   
            }
        }

        if (item == null)
            Debug.LogWarning($"configs with \"{itemID}\" ID not found");

        return item;
    }

    public Item CreateRandomItem()
    {
        int rand = UnityEngine.Random.Range( 0, _configs.Length);
        Item item = CreateItemByID(_configs[rand].ID);

        return item;
    }
}
