using System.Collections.Generic;
using UnityEngine;

// - The Main GameSystem
// - Using a FiniteStateMachine (FSM), Decorator and Command Pattern
// - Daniel Bruijn

public class GameSystem : MonoBehaviour
{
    // - Variables
    [Header("Player References")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Rigidbody playerRigidbody;
    
    private Player _player;
    private PlayerStateMachine _playerStateMachine;

    [Header("Gun References")] 
    private GunStateMachine _gunStateMachine;

    [Header("Targets")]
    [SerializeField] private Transform[] targetTransforms;
    
    // - Input
    private InputHandler _inputHandler;
    
    // - Target
    private List<TargetActor> _targets;

    void Start()
    {
        // - Cursor Lock
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        // - References
        InitializePlayer();
        InitializeTargets();
        InitializeGuns();
        
        // - Input
        _inputHandler = new InputHandler(_gunStateMachine);
        
        // - Debug
        IWeapon weapon = new Rifle();
        Debug.Log(weapon.GetDamage());
        Debug.Log(weapon.GetMaxAmmo());

        weapon = new DamageBoost(weapon);
        Debug.Log(weapon.GetDamage());

        weapon = new MagazineBoost(weapon);
        Debug.Log(weapon.GetMaxAmmo());
    }

    void Update()
    {
        UpdatePlayer();
        UpdateGun();
        
        // - ICommand
        ICommand command = _inputHandler.GetCommand();
        if (command != null)
            command.Execute();
    }

    void InitializePlayer()
    {
        // - Player - FSM
        _player = new Player(playerTransform, cameraTransform, playerRigidbody);
        
        _playerStateMachine = new PlayerStateMachine(_player);
        _playerStateMachine.ChangeState(new PlayerIdleState());

    }

    void InitializeTargets()
    {
        // - Targets
        _targets = new List<TargetActor>();
        foreach (Transform targetTransform in targetTransforms)
        {
            Target target = new Target(30);
            
            _targets.Add(new TargetActor(targetTransform, target));
        }
    }

    void InitializeGuns()
    {
        // - Gun - FSM
        Weapon rifle = new Rifle();

        _gunStateMachine = new GunStateMachine(rifle, _player, _targets);
    }

    void UpdatePlayer()
    {
        // - Player Update
        _player.Look(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        _playerStateMachine.Update();
    }

    void UpdateGun()
    {
        // - Gun Input
        bool held = Input.GetMouseButton(0);
        bool pressed = Input.GetMouseButtonDown(0);
        
        _gunStateMachine.HandleShooting(held, pressed);
        
        // - Gun State
        _gunStateMachine.Update();
    }
}