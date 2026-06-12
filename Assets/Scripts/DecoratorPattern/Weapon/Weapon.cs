using UnityEngine;

// - Base Class for the Weapons
// - Used to return states like Damage, FireRate and Recoil
// - Daniel Bruijn

public class Weapon : IWeapon
{
    // - Variables
    protected int damage;
    protected int ammo;
    protected int maxAmmo;
    
    public virtual int GetDamage()
    {
        return damage;
    }

    public virtual int GetAmmo()
    {
        return ammo;
    }

    public virtual bool Shoot()
    {
        if (ammo <= 0)
        {
            Debug.Log("No ammo left :(");
            return false;
        }
        
        ammo--;
        return true;
    }

    public virtual void Reload()
    {
        ammo = maxAmmo;
    }
}
