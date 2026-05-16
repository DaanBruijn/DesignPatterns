using UnityEngine;

public class ReloadCommand : ICommand
{
    public void Execute()
    {
        Debug.Log("Reload.Log");
    }
}
