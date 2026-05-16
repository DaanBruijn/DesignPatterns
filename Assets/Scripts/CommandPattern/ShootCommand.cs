using UnityEngine;
using UnityEngine.Rendering.UI;

public class ShootCommand : ICommand
{
    public void Execute()
    {
        Debug.Log("Shoot.Log");
    }
}