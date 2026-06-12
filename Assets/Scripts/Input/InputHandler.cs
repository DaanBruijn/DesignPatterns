using UnityEngine;

// - Script to handle the Input for the Command pattern
// - Daniel bruijn

public class InputHandler
{
    // - Variables
    private GunStateMachine _gunStateMachine;
    
    public InputHandler(GunStateMachine gunStateMachine)
    {
        _gunStateMachine = gunStateMachine;
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

        // - Weapon Upgrades
        if (Input.GetKeyDown(KeyCode.Alpha8))
            return new UpgradeDamageCommand(_gunStateMachine);
        
        if (Input.GetKeyDown(KeyCode.Alpha9))
            return new UpgradeMagazineCommand(_gunStateMachine);
        
        if (Input.GetKeyDown(KeyCode.Alpha0))
            return new UpgradeFireRateCommand(_gunStateMachine);

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
