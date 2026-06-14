// - Shoot Command - Inheritance from ICommand
// - Daniel Bruijn

public class ShootCommand : ICommand
{
    // - Variables
    private GunStateMachine _gunStateMachine;
    
    public ShootCommand(GunStateMachine _gunStateMachine)
    {
        this._gunStateMachine = _gunStateMachine;
    }
    
    public void Execute()
    {
        _gunStateMachine.ChangeGunState(new FiringState());
    }
}