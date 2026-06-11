using UnityEngine;

// - Crouching State for the Player
// - Used by PlayerState
// - Daniel Bruijn

public class CrouchingState : PlayerState
{
    public override void Enter(PlayerStateMachine stateMachine)
    {
        Debug.Log("Player Entered Crouching");
    }

    public override void Update(PlayerStateMachine stateMachine)
    {
        Vector3 input = stateMachine.Player.GetMovementInput();
        
        stateMachine.Player.Move(input, stateMachine.Player.crouchSpeed);
        
        if (!Input.GetKey(KeyCode.LeftControl))
        {
            stateMachine.ChangeState(new PlayerIdleState());
        }
    }

    public override void Exit(PlayerStateMachine stateMachine)
    {
    }
}
