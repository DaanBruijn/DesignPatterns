// - System used for handling Upgrades after run
// - Daniel Bruijn

public class UpgradeSystem
{
    private GunStateMachine _gunStateMachine;

    public UpgradeSystem(GunStateMachine gunStateMachine)
    {
        _gunStateMachine = gunStateMachine;
    }

    public void ApplyDamageUpgrade()
    {
        IWeapon upgraded = new DamageBoost(_gunStateMachine.CurrentWeapon);
        _gunStateMachine.ApplyUpgrade(upgraded);
    }

    public void ApplyMagazineUpgrade()
    {
        IWeapon upgraded = new MagazineBoost(_gunStateMachine.CurrentWeapon);
        _gunStateMachine.ApplyUpgrade(upgraded);
    }

    public void ApplyFireRateUpgrade()
    {
        IWeapon upgraded = new FireRateBoost(_gunStateMachine.CurrentWeapon);
        _gunStateMachine.ApplyUpgrade(upgraded);
    }
}
