using UnityEngine;

// - Firing State for the gun
// - Used by GunState
// - Daniel Bruijn

public class FiringState : GunState
{
    public override void Enter(GunStateMachine stateMachine)
    {
        // - Try shooting when entering
        TryShoot(stateMachine);
        stateMachine.ChangeGunState(new GunIdleState());
    }

    public override void Update(GunStateMachine stateMachine)
    {
    }

    public override void Exit(GunStateMachine stateMachine)
    {
    }

    private void TryShoot(GunStateMachine stateMachine)
    {
        if (Time.time < stateMachine.NextFireTime)
            return;
        
        if (!stateMachine.CurrentWeapon.Shoot())
        {
            stateMachine.ChangeGunState(new GunIdleState());
            return;
        }
        
        stateMachine.NextFireTime = Time.time + stateMachine.CurrentWeapon.GetFireRate();
        
        if (stateMachine.Player.TryRayCast(out RaycastHit hit))
        {
            TargetActor target = stateMachine.GetTarget(hit.transform);

            if (target != null)
                target.TargetData.TakeDamage(stateMachine.CurrentWeapon.GetDamage());
            
            if (target.TargetData.IsDestroyed)
                target.DestroyTarget();
        }
    }
}
