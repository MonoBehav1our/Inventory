using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;
using Model.Inventory;


public class Gun : MonoBehaviour
{
    public event Action OnShootEnd;

    [SerializeField] private int _damage;
    [SerializeField] private int _shootAmount;
    [SerializeField] private AmmoType _type;

    private Image _image;
    private InventoryGrid _inventory;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void Init(InventoryGrid inventory)
    {
        _inventory = inventory;
    }

    public IEnumerator ShootRoutine(Unit target, bool useBullets)
    {
        for (int i = 0; i < _shootAmount; i++)
        {
            if (useBullets)
            {
                if (_inventory.TryUseBullet(_type))

                    target.GetDamage(_damage);
                yield return new WaitForSeconds(0.3f);
            }
            else
            {
                target.GetDamage(_damage);
                yield return new WaitForSeconds(0.3f);
            }
        }
        OnShootEnd?.Invoke();
    }

    public void Select()
    {
        _image.color = Color.white;
    }
    public void DeSelect()
    {
        _image.color = Color.gray;
    }
}
