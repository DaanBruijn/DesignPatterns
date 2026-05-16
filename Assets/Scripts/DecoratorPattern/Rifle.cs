using UnityEngine;

// - Rifle Class - Inheritance from IWeapon
// - Daniel Bruijn

public class Rifle : IWeapon
{
    public int GetDamage()
    {
        return 15;
    }
}
