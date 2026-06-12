using UnityEngine;
// - Command for Upgrading the MagazineSize
// - Daniel Bruijn

public class UpgradeMagazineCommand : ICommand
{
    // - Variables
    private UpgradeSystem _upgradeSystem;

    public UpgradeMagazineCommand(UpgradeSystem upgradeSystem)
    {
        _upgradeSystem = upgradeSystem;
    }

    public void Execute()
    {
        _upgradeSystem.ApplyMagazineUpgrade();
    }
}