// - Pistol Class - Inheritance from Weapon
// - Daniel Bruijn

public class Pistol : Weapon
{
    public Pistol()
    {
        fireMode = FireMode.SemiAuto;
        
        damage = 10;
        maxAmmo = 12;
        fireRate = 0.25f;
    }
}
