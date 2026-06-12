// - Command for Equipping your Pistol
// - Daniel Bruijn

public class EquipPistolCommand : ICommand
{
    private GunStateMachine _gunStateMachine;

    public EquipPistolCommand(GunStateMachine gunStateMachine)
    {
        _gunStateMachine = gunStateMachine;
    }

    public void Execute()
    {
        _gunStateMachine.EquipPistol();
    }
}