using UnityEngine;

// - PlayerStateMachine for the Player States
// - Daniel Bruijn

public class PlayerStateMachine
{
    // - Variables
    private PlayerState currentState;
    
    public Player Player { get; }

    public PlayerStateMachine(Player player)
    {
        Player = player;
    }

    public void ChangeState(PlayerState newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }

    public void Update()
    {
        currentState?.Update(this);
    }
}
