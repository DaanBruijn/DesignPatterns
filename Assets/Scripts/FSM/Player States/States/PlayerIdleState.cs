using UnityEngine;
using UnityEngine.InputSystem;

// - PlayerIdle State for the Player
// - Used by PlayerState
// - Daniel Bruijn

public class PlayerIdleState : PlayerState
{
    public override void Enter(PlayerStateMachine stateMachine)
    {
        Debug.Log("Player Idle");
    }

    public override void Update(PlayerStateMachine stateMachine)
    {
        Vector3 input = stateMachine.Player.GetMovementInput();

        if (input != Vector3.zero)
        {
            stateMachine.ChangeState(new WalkingState());
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && stateMachine.Player.IsGrounded())
        {
            stateMachine.ChangeState(new JumpingState());
        }
    }

    public override void Exit(PlayerStateMachine stateMachine)
    {
    }
}
