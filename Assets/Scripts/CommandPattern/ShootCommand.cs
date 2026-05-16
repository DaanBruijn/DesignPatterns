using UnityEngine;
using UnityEngine.Rendering.UI;

// - Shoot Command - Inheritance from ICommand
// - Daniel Bruijn

public class ShootCommand : ICommand
{
    public void Execute()
    {
        Debug.Log("Shoot.Log");
    }
}