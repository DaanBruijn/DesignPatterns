// - FireRateBoost Class - Inheritance from WeaponDecorator
// - Used to change weapon firerate stat
// - Daniel Bruijn

public class FireRateBoost : WeaponDecorator
{
    public FireRateBoost(IWeapon weapon) : base(weapon)
    {
    }

    public override float GetFireRate()
    {
        return base.GetFireRate() * 0.8f;
    }
}
