// - MagazineBoost Class - Inheritance from WeaponDecorator
// - Used to change weapon Ammo stat
// - Daniel Bruijn

public class MagazineBoost : WeaponDecorator
{
    public MagazineBoost(IWeapon weapon) : base(weapon)
    {
    }

    public override int GetMaxAmmo()
    {
        return weapon.GetMaxAmmo() + 10;
    }

    public override void Reload()
    {
        weapon.SetAmmo(GetMaxAmmo());
    }
}
