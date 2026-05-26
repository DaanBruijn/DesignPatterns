using UnityEngine;

// - The Main GameSystem
// - Using a FiniteStateMachine (FSM), Decorator and Command Pattern
// - Daniel Bruijn

public class GameSystem : MonoBehaviour
{
    // - States
    private GunState currentState;

    void Start()
    {
        // - FSM
        currentState = GunState.Idle;

        // - Decorator
        IWeapon pistol = new Pistol();
        Debug.Log("Pistol Base Damage= " + pistol.GetDamage());
        pistol = new DamageBoost(pistol);
        Debug.Log("Pistol Boosted Damage= " + pistol.GetDamage());
        
        IWeapon rifle  = new Rifle();
        Debug.Log("Rifle Base Damage= " + rifle.GetDamage());
        rifle = new DamageBoost(rifle);
        Debug.Log("Rifle Boosted Damage= " + rifle.GetDamage());
    }

    void Update()
    {
        HandleStateMachine();
        HandleCommands();
    }

    private void HandleStateMachine()
    {
        switch (currentState)
        {
            case GunState.Idle:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    currentState = GunState.Firing;
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    currentState = GunState.Reloading;
                }

                break;

            case GunState.Firing:
                Debug.Log("Firing State active");
                currentState = GunState.Idle;

                break;

            case GunState.Reloading:
                Debug.Log("Reloading State active");
                currentState = GunState.Idle;

                break;
        }
    }

    private void HandleCommands()
    {
        ICommand command = null;
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            command = new ShootCommand();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            command = new ReloadCommand();
        }
        if (command != null)
        {
            command.Execute();
        }
    }
}