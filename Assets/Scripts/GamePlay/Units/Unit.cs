using System;


public abstract class Unit
{
    public Gun Gun;

    public event Action OnHealthChanged;
    public event Action OnDie;

    public const int MAX_HEALTH = 100;
    public int Health => _health;
    protected int _health;

    public Unit(Gun gun)
    {
        Gun = gun;
        _health = MAX_HEALTH;
    }

    public abstract void GetDamage(int damage);

    protected void InvokeOnDieEvent() => OnDie?.Invoke();
    protected void InvokeOnHealthChanged() => OnHealthChanged?.Invoke();
}
