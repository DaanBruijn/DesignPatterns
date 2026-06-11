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
    
    // - Input
    private InputHandler _inputHandler;

    void Start()
    {
        // - Cursor Lock
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        // - References
        // - Gun - FSM
        Weapon pistol = new Pistol();

        _gunStateMachine = new GunStateMachine(pistol);
        
        // - Input
        _inputHandler = new InputHandler(_gunStateMachine);
        
        // - Player - FSM
        _player = new Player(playerTransform, cameraTransform, playerRigidbody);
        
        _playerStateMachine = new PlayerStateMachine(_player);
        _playerStateMachine.ChangeState(new PlayerIdleState());
    }

    void Update()
    {
        // - Player Update
        _player.Look(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        
        _playerStateMachine.Update();
        
        // - Gun Update
        _gunStateMachine.Update();
        
        // - ICommand
        ICommand command = _inputHandler.GetCommand();
        
        if (command != null)
            command.Execute();
    }
}