using UnityEngine;

// - Pistol Class - Inheritance from Weapon
// - Daniel Bruijn

public class Pistol : Weapon
{
    public Pistol()
    {
        damage = 10;
        maxAmmo = 12;
        ammo = maxAmmo;
    }
}
