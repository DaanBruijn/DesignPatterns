// - FSM Pattern for the PlayerStates
// - Daniel Bruijn

public abstract class PlayerState
{
    public abstract void Enter(PlayerStateMachine stateMachine);
    public abstract void Update(PlayerStateMachine stateMachine);
    public abstract void Exit(PlayerStateMachine stateMachine);
}
