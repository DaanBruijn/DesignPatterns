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

    public FireMode GetFireMode()
    {
        return weapon.GetFireMode();
    }

    public virtual int GetDamage()
    {
        return weapon.GetDamage();
    }

    public virtual bool Shoot()
    {
        return weapon.Shoot();
    }

    public virtual void Reload()
    {
        weapon.Reload();
    }

    public virtual int GetAmmo()
    {
        return weapon.GetAmmo();
    }

    public virtual int GetMaxAmmo()
    {
        return weapon.GetMaxAmmo();
    }

    public virtual float GetFireRate()
    {
        return weapon.GetFireRate();
    }
}
