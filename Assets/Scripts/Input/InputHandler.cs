using UnityEngine;

// - Script to handle the Input for the Command pattern
// - Daniel bruijn

public class InputHandler
{
    // - Variables
    private GunStateMachine _gunStateMachine;
    private UpgradeSystem _upgradeSystem;
    
    public InputHandler(GunStateMachine gunStateMachine,  UpgradeSystem upgradeSystem)
    {
        _gunStateMachine = gunStateMachine;
        _upgradeSystem = upgradeSystem;
    }
    
    public ICommand GetCommand()
    {
        // - Weapon Shooting / Reload
        if (Input.GetMouseButtonDown(0))
            return new ShootCommand(_gunStateMachine);

        if (Input.GetKeyDown(KeyCode.R))
            return new ReloadCommand(_gunStateMachine);

        // - Weapon Equip
        if (Input.GetKeyDown(KeyCode.Alpha1))
            return new EquipRifleCommand(_gunStateMachine);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            return new EquipPistolCommand(_gunStateMachine);

        return null;
    }
    
    public ICommand GetUpgradeCommand()
    {
        // - Weapon Upgrades
        if (Input.GetKeyDown(KeyCode.Alpha8))
            return new UpgradeDamageCommand(_upgradeSystem);
        
        if (Input.GetKeyDown(KeyCode.Alpha9))
            return new UpgradeMagazineCommand(_upgradeSystem);
        
        if (Input.GetKeyDown(KeyCode.Alpha0))
            return new UpgradeFireRateCommand(_upgradeSystem);

        return null;
    }

    public bool ShootHeld()
    {
        return Input.GetMouseButton(0);
    }

    public bool ShootPressed()
    {
        return Input.GetMouseButtonDown(0);
    }

    
}
