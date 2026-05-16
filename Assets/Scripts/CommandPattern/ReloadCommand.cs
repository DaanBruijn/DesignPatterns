using UnityEngine;

// - Reload Command - Inheritance from ICommand
// - Daniel Bruijn

public class ReloadCommand : ICommand
{
    public void Execute()
    {
        Debug.Log("Reload.Log");
    }
}
