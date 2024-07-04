using UnityEngine;
using System;


public class Enemy : Unit
{
    public Enemy(Gun gun) : base(gun) 
    {
        Gun = gun;
    }

    public override void GetDamage(int damage)
    {
        {
            if (damage <= 0)
                throw new InvalidOperationException("Damage should be more than 0");

            _health -= damage;
            _health = Mathf.Clamp(_health, 0, MAX_HEALTH);
            InvokeOnHealthChanged();
            Debug.Log("Enemy: " + (float)Health / (float)Unit.MAX_HEALTH);

            if (_health <= 0)
            {
                InvokeOnDieEvent();
                _health = MAX_HEALTH;
            }
        }
    }
}
