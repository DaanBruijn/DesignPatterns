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

    public virtual int GetDamage() => weapon.GetDamage();

    public virtual bool Shoot() => weapon.Shoot();
    
    public virtual int GetAmmo() => weapon.GetAmmo();
    
    public virtual int GetMaxAmmo() => weapon.GetMaxAmmo();

    public virtual void Reload() => weapon.Reload();
    
    public virtual float GetFireRate()
    {
        return weapon.GetFireRate();
    }
}
