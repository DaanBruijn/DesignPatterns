using UnityEngine;

// - Rifle Class - Inheritance from Weapon
// - Daniel Bruijn

public class Rifle : Weapon
{
    public Rifle()
    {
        damage = 15;
        maxAmmo = 30;
        ammo = maxAmmo;
    }
}
