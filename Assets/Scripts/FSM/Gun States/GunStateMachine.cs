using System.Collections.Generic;
using UnityEngine;

// - GunStateMachine for the GunStates
// - Daniel Bruijn

public class GunStateMachine
{
    // - Variables
    private GunState _currentState;

    private float _nextFireTime;

    private IWeapon _rifle;
    private IWeapon _pistol;

    public IWeapon CurrentWeapon { get; private set; }
    public Player Player { get; }

    public float NextFireTime
    {
        get => _nextFireTime;
        set => _nextFireTime = value;
    }

    private List<TargetActor> _targets;

    public GunStateMachine(IWeapon rifle, IWeapon pistol, Player player, List<TargetActor> targets)
    {
        _rifle = rifle;
        _pistol = pistol;

        CurrentWeapon = rifle;

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

    public void ApplyUpgrade(IWeapon upgradedWeapon)
    {
        // - !! Change to switch case if more weapons are added !!
        if (CurrentWeapon == _rifle)
        {
            _rifle = upgradedWeapon;
            CurrentWeapon = _rifle;
        }
        else
        {
            _pistol = upgradedWeapon;
            CurrentWeapon = _pistol;
        }
    }

    public void SetWeapon(IWeapon newWeapon)
    {
        CurrentWeapon = newWeapon;
    }

    public void EquipRifle()
    {
        CurrentWeapon = _rifle;
        Debug.Log("Equip Rifle");
    }

    public void EquipPistol()
    {
        CurrentWeapon = _pistol;
        Debug.Log("Equip Pistol");
    }

    public bool IsRifleEquipped()
    {
        return CurrentWeapon == _rifle;
    }
    
    public bool IsPistolEquipped()
    {
        return CurrentWeapon == _pistol;
    }
}
