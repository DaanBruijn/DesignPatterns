// - PlayerStateMachine for the Player States
// - Daniel Bruijn

public class PlayerStateMachine
{
    // - Variables
    private PlayerState _currentState;
    
    public Player Player { get; }

    public PlayerStateMachine(Player player)
    {
        Player = player;
    }

    public void ChangeState(PlayerState newState)
    {
        _currentState?.Exit(this);
        _currentState = newState;
        _currentState.Enter(this);
    }

    public void Update()
    {
        _currentState?.Update(this);
    }
}
