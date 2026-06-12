using UnityEngine;
// - Command for Upgrading the FireRate
// - Daniel Bruijn

public class UpgradeFireRateCommand : ICommand
{
    // - Variables
    private GunStateMachine _gunStateMachine;

    public UpgradeFireRateCommand(GunStateMachine gunStateMachine)
    {
        _gunStateMachine = gunStateMachine;
    }

    public void Execute()
    {
        _gunStateMachine.ApplyUpgrade(new FireRateBoost(_gunStateMachine.CurrentWeapon));
        
        Debug.Log("FireRate boost applied");
    }
}