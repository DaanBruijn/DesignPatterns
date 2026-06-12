// - Weapon Interface Base
// - Daniel Bruijn

public interface IWeapon
{ 
    FireMode GetFireMode();
    int GetDamage();
    int GetAmmo();
    int GetMaxAmmo();
    float GetFireRate();

    bool Shoot();
    void Reload();
    void SetAmmo(int ammo);
}
