using UnityEngine;

// - Pistol Class - Inheritance from IWeapon
// - Daniel Bruijn

public class Pistol : IWeapon
{
    public int GetDamage()
    {
        return 10;
    }
}
