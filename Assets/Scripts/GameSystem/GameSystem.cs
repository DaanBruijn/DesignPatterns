using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    
    [Header("Gun Models")]
    [SerializeField] private GameObject rifleModel;
    [SerializeField] private GameObject pistolModel;

    [Header("Targets")]
    [SerializeField] private Transform[] targetTransforms;

    [Header("UI")] 
    [SerializeField] private TMP_Text ammoText;
    
    // - Input
    private InputHandler _inputHandler;
    
    // - Target
    private List<TargetActor> _targets;
    
    // - Timer
    private float _runTimer;
    private bool _runActive;

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
        
        // - Timer
        _runActive = true;
        _runTimer = 0;
    }

    void Update()
    {
        // - Player
        UpdatePlayer();
        
        // - ICommand
        ICommand command = _inputHandler.GetCommand();
        if (command != null)
            command.Execute();
        
        // - Guns
        UpdateGun();
        UpdateWeaponVisuals();
        
        // - UI
        UpdateAmmoUI();
        
        // - RunTimer
        UpdateRun();
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
            Target target = new Target(100);
            
            _targets.Add(new TargetActor(targetTransform, target));
        }
    }

    void InitializeGuns()
    {
        // - Gun - FSM
        IWeapon rifle = new Rifle();
        IWeapon pistol = new Pistol();

        _gunStateMachine = new GunStateMachine(rifle, pistol, _player, _targets);
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

    void UpdateWeaponVisuals()
    {
        rifleModel.SetActive(_gunStateMachine.IsRifleEquipped());
        pistolModel.SetActive(_gunStateMachine.IsPistolEquipped());
    }

    void UpdateAmmoUI()
    {
        ammoText.text = _gunStateMachine.CurrentWeapon.GetAmmo() + " / " + _gunStateMachine.CurrentWeapon.GetMaxAmmo();
    }

    void UpdateRun()
    {
        if (_runActive)
            _runTimer += Time.deltaTime;

        if (_runActive && AreAllTargetsDestroyed())
            CompleteRun();
    }

    void CompleteRun()
    {
        _runActive = false;
        Debug.Log("Time: " + _runTimer.ToString("F2"));
    }

    bool AreAllTargetsDestroyed()
    {
        foreach (TargetActor target in _targets)
        {
            if (!target.TargetData.IsDestroyed)
                return false;
        }

        return true;
    }
}