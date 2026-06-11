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
        if (Input.GetMouseButtonDown(0))
        {
            return new ShootCommand(_gunStateMachine);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            return new ReloadCommand(_gunStateMachine);
        }

        return null;
    }
}
