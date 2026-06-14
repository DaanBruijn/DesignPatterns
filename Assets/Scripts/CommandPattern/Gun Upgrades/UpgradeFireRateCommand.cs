// - Command for Upgrading the FireRate
// - Daniel Bruijn

public class UpgradeFireRateCommand : ICommand
{
    // - Variables
    private UpgradeSystem _upgradeSystem;

    public UpgradeFireRateCommand(UpgradeSystem upgradeSystem)
    {
        _upgradeSystem = upgradeSystem;
    }

    public void Execute()
    {
        _upgradeSystem.ApplyFireRateUpgrade();
    }
}