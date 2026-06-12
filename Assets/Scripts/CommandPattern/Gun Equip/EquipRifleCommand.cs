// - Command for Equipping your Rifle
// - Daniel Bruijn

public class EquipRifleCommand : ICommand
{
    private GunStateMachine _gunStateMachine;

    public EquipRifleCommand(GunStateMachine gunStateMachine)
    {
        _gunStateMachine = gunStateMachine;
    }

    public void Execute()
    {
        _gunStateMachine.EquipRifle();
    }
}
