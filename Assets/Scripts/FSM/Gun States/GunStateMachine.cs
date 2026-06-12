using System.Collections.Generic;
using UnityEngine;

// - GunStateMachine for the GunStates
// - Daniel Bruijn

public class GunStateMachine
{
    // - Variables
    private GunState _currentState;

    private float _nextFireTime;
    
    public IWeapon CurrentWeapon { get; private set;  }
    public Player Player { get; }
    
    public float NextFireTime
    {
        get => _nextFireTime;
        set => _nextFireTime = value;
    }
    
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

    public void HandleShooting(bool shootHeld, bool shootPressed)
    {
        if (CurrentWeapon.GetFireMode() == FireMode.FullAuto)
        {
            if (shootHeld)
                ChangeGunState(new FiringState());
        }
        else
        {
            if (shootPressed)
                ChangeGunState(new FiringState());
        }
    }

    public void ApplyUpgrade(IWeapon newWeapon)
    {
        CurrentWeapon = newWeapon;
    }
}
