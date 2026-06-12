using UnityEngine;
// - Command for Upgrading the WeaponDamage
// - Daniel Bruijn

public class UpgradeDamageCommand : ICommand
{
    // - Variables
    private GunStateMachine _gunStateMachine;

    public UpgradeDamageCommand(GunStateMachine gunStateMachine)
    {
        _gunStateMachine = gunStateMachine;
    }

    public void Execute()
    {
        _gunStateMachine.ApplyUpgrade(new DamageBoost(_gunStateMachine.CurrentWeapon));
        
        Debug.Log("Damage boost applied");
    }
}
