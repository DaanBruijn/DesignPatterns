using UnityEngine;

// - Reload Command - Inheritance from ICommand
// - Daniel Bruijn

public class ReloadCommand : ICommand
{
    // - Variables
    private GunStateMachine _gunStateMachine;


    public ReloadCommand(GunStateMachine _gunStateMachine)
    {
        this._gunStateMachine = _gunStateMachine;
    }
    
    public void Execute()
    {
        _gunStateMachine.ChangeGunState(new ReloadingState());
    }
}
