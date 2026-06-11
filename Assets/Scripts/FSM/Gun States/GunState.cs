using UnityEngine;

// - FSM Pattern for the GunStates
// - Daniel Bruijn

public abstract class GunState
{
    public abstract void Enter(GunStateMachine stateMachine);
    public abstract void Update(GunStateMachine stateMachine);
    public abstract void Exit (GunStateMachine stateMachine);
}
