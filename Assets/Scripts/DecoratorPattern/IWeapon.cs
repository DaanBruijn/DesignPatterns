// - Weapon Interface Base
// - Daniel Bruijn

public interface IWeapon
{ 
    int GetDamage();
    int GetAmmo();
    int GetMaxAmmo();
    float GetFireRate();

    bool Shoot();
    void Reload();
}
