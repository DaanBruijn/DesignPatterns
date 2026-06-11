using UnityEngine;

// - Running State for the Player
// - Used by PlayerState
// - Daniel Bruijn

public class RunningState : PlayerState
{
    public override void Enter(PlayerStateMachine stateMachine)
    {
        Debug.Log("Player Running");
    }

    public override void Update(PlayerStateMachine stateMachine)
    {
        Vector3 input = stateMachine.Player.GetMovementInput();
        
        stateMachine.Player.Move(input, stateMachine.Player.runSpeed);
        
        if (input == Vector3.zero)
        {
            stateMachine.ChangeState(new PlayerIdleState());
            return;
        }

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            stateMachine.ChangeState(new WalkingState());
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