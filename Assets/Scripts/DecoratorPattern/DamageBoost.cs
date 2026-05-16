using UnityEngine;

// - DamageBoost Class - Inheritance from WeaponDecorator
// - Used to change weapon damage stat
// - Daniel Bruijn

public class DamageBoost : WeaponDecorator
{
    public DamageBoost(IWeapon weapon) : base(weapon)
    {
    }

    public override int GetDamage()
    {
        return weapon.GetDamage() + 5;
    }
}
