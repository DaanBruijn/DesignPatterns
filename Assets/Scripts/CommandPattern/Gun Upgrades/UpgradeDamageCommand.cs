using UnityEngine;
// - Command for Upgrading the WeaponDamage
// - Daniel Bruijn

public class UpgradeDamageCommand : ICommand
{
    // - Variables
    private UpgradeSystem _upgradeSystem;

    public UpgradeDamageCommand(UpgradeSystem upgradeSystem)
    {
        _upgradeSystem = upgradeSystem;
    }

    public void Execute()
    {
        _upgradeSystem.ApplyDamageUpgrade();
    }
}
