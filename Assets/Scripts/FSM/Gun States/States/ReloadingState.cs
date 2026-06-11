using UnityEngine;

// - Reloading State for the gun
// - Used by GunState
// - Daniel Bruijn

public class ReloadingState : GunState
{
    public override void Enter(GunStateMachine stateMachine)
    {
        stateMachine.CurrentWeapon.Reload();
        
        Debug.Log("Reloading");
        
        stateMachine.ChangeGunState(new GunIdleState());
    }

    public override void Update(GunStateMachine stateMachine)
    {
    }

    public override void Exit(GunStateMachine stateMachine)
    {
    }
}