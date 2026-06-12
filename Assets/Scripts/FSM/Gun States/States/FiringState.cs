using UnityEngine;

// - Firing State for the gun
// - Used by GunState
// - Daniel Bruijn

public class FiringState : GunState
{
    public override void Enter(GunStateMachine stateMachine)
    {
        if (!stateMachine.CurrentWeapon.Shoot())
        {
            stateMachine.ChangeGunState(new GunIdleState());
            return;
        }
        
        if (stateMachine.Player.TryRayCast(out RaycastHit hit))
        {
            TargetActor target = stateMachine.GetTarget(hit.transform);

            if (target != null)
            {
                target.TargetData.TakeDamage(stateMachine.CurrentWeapon.GetDamage());
            }
        }
        
        stateMachine.ChangeGunState(new GunIdleState());
    }

    public override void Update(GunStateMachine stateMachine)
    {
    }

    public override void Exit(GunStateMachine stateMachine)
    {
    }
}
