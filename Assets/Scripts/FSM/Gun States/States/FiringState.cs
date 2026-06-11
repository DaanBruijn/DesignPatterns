using UnityEngine;

// - Firing State for the gun
// - Used by GunState
// - Daniel Bruijn

public class FiringState : GunState
{
    public override void Enter(GunStateMachine stateMachine)
    {
        stateMachine.CurrentWeapon.Shoot();
        
        Debug.Log("Shooting - Ammon: " + stateMachine.CurrentWeapon.GetAmmo());
        
        stateMachine.ChangeGunState(new GunIdleState());
    }

    public override void Update(GunStateMachine stateMachine)
    {
    }

    public override void Exit(GunStateMachine stateMachine)
    {
    }
}
