// - Rifle Class - Inheritance from Weapon
// - Daniel Bruijn

public class Rifle : Weapon
{
    public Rifle()
    {
        fireMode = FireMode.FullAuto;
        
        damage = 15;
        maxAmmo = 30;
        fireRate = 0.1f;
    }
}
