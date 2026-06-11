using UnityEngine;

// - Jumping State for the Player
// - Used by PlayerState
// - Daniel Bruijn

public class JumpingState : PlayerState
{
    private float _jumpDelay;
    
    public override void Enter(PlayerStateMachine stateMachine)
    {
        Debug.Log("Player Jumping");
        stateMachine.Player.Jump();

        _jumpDelay = 0.15f;
    }

    public override void Update(PlayerStateMachine stateMachine)
    {
        _jumpDelay -= Time.deltaTime;
        
        if (_jumpDelay > 0f)
            return;
        
        if (stateMachine.Player.IsGrounded())
        {
            stateMachine.ChangeState(new PlayerIdleState());
        }
    }

    public override void Exit(PlayerStateMachine stateMachine)
    {
    }
}