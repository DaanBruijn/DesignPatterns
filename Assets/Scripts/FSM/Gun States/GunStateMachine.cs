using UnityEngine;

// - GunStateMachine for the GunStates
// - Daniel Bruijn

public class GunStateMachine
{
    // - Variables
    private GunState _currentState;
    
    public Weapon CurrentWeapon { get; }

    public GunStateMachine(Weapon weapon)
    {
        CurrentWeapon = weapon;
    }

    public void ChangeGunState(GunState newState)
    {
        _currentState?.Exit(this);
        _currentState = newState;
        _currentState?.Enter(this);
    }

    public void Update()
    {
        _currentState?.Update(this);
    }
}
