using Model.Inventory;
using UnityEngine;
using System;


public class Player : Unit
{
    private InventorySlot<HeaddressItem> _headSlot;
    private InventorySlot<OutwearItem> _outwearSlot;

    public Player(Gun gun, InventorySlot<HeaddressItem> headSlot, InventorySlot<OutwearItem> outwearSlot) : base(gun)
    {
        this.Gun = gun; 

        _headSlot = headSlot;
        _outwearSlot = outwearSlot;
    }

    public void AddHealth(int health)
    {
        if (health <= 0)
            throw new InvalidOperationException("Health for healing should be more than 0");

        _health += health;
        _health = Mathf.Clamp(_health, 0, MAX_HEALTH);
    }

    public override void GetDamage(int damage)
    {
        if (damage <= 0)
            throw new InvalidOperationException("Damage should be more than 0");

        int rand = UnityEngine.Random.Range(0, 2);
        IEquipmentItem tempSlot = null;

        switch (rand)
        {
            case 0:
                tempSlot = _headSlot.Item;
                break;
            case 1:
                tempSlot = _outwearSlot.Item;
                break;
        }

        if (tempSlot != null)
            damage -= tempSlot.ArmorValue;


        _health -= damage;
        _health = Mathf.Clamp(_health, 0, MAX_HEALTH);
        InvokeOnHealthChanged();
        Debug.Log("Player: " + (float)Health / (float)Unit.MAX_HEALTH);

        if (_health <= 0)
        {
            InvokeOnDieEvent();
            _health = MAX_HEALTH;
        }
    }
}
