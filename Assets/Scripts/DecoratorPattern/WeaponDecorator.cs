using UnityEngine;

// - Decorator Pattern Base - Inheritance from IWeapon
// - Used to change weapon stats
// - Daniel Bruijn

public class WeaponDecorator : IWeapon
{
    // - Variables
    protected IWeapon weapon;

    public WeaponDecorator(IWeapon weapon)
    {
        this.weapon = weapon;
    }

    public virtual int GetDamage()
    {
        return weapon.GetDamage();
    }
}
