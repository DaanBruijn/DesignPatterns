using UnityEngine;

// - Walking State for the Player
// - Used by PlayerState
// - Daniel Bruijn

public class WalkingState : PlayerState
{
    public override void Enter(PlayerStateMachine stateMachine)
    {
        Debug.Log("Player Walking");
    }

    public override void Update(PlayerStateMachine stateMachine)
    {
        Vector3 input = stateMachine.Player.GetMovementInput();
        
        stateMachine.Player.Move(input, stateMachine.Player.walkSpeed);

        if (input == Vector3.zero)
        {
            stateMachine.ChangeState(new PlayerIdleState());
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            stateMachine.ChangeState(new RunningState());
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
