using System.Collections.Generic;
using UnityEngine;

// - GunStateMachine for the GunStates
// - Daniel Bruijn

public class GunStateMachine
{
    // - Variables
    private GunState _currentState;
    
    public Weapon CurrentWeapon { get; }
    public Player Player { get; }
    
    private List<TargetActor> _targets;

    public GunStateMachine(Weapon weapon, Player player, List<TargetActor> targets)
    {
        CurrentWeapon = weapon;
        Player = player;
        _targets = targets;
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

    public TargetActor GetTarget(Transform transform)
    {
        foreach (TargetActor target in _targets)
        {
            if (target.Transform == transform)
            {
                return target;
            }
        }
        
        return null;
    }
}
