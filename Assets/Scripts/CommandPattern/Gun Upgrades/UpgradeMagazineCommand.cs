using UnityEngine;
// - Command for Upgrading the MagazineSize
// - Daniel Bruijn

public class UpgradeMagazineCommand : ICommand
{
    // - Variables
    private GunStateMachine _gunStateMachine;

    public UpgradeMagazineCommand(GunStateMachine gunStateMachine)
    {
        _gunStateMachine = gunStateMachine;
    }

    public void Execute()
    {
        _gunStateMachine.ApplyUpgrade(new MagazineBoost(_gunStateMachine.CurrentWeapon));
        
        Debug.Log("Magazine boost applied");
    }
}