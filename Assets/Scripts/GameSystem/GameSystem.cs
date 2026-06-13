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
    
    [Header("Gun Models")]
    [SerializeField] private GameObject rifleModel;
    [SerializeField] private GameObject pistolModel;
    
    [Header("Targets")]
    [SerializeField] private Transform[] targetTransforms;
    
    [Header("Startzone")]
    [SerializeField] private Transform _startZone;
    [SerializeField] private float _startRadius = 2f;
    
    [Header("UI")]
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private TMP_Text upgradeText;
    [SerializeField] private TMP_Text runInfoText;
    [SerializeField] private TMP_Text bestRunTimeText;
    
    private Player _player;
    private PlayerStateMachine _playerStateMachine;
    
    private GunStateMachine _gunStateMachine;
    private UpgradeSystem _upgradeSystem;
    private InputHandler _inputHandler;
    
    private RunManager _runManager;
    private TargetManager _targetManager;
    private UIManager _uiManager;
    
    private bool _inUpgradePhase;
    
    void Start()
    {
        // - Cursor Lock
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        // - References
        InitializePlayer();
        InitializeTargets();
        InitializeGuns();
        InitializeSystems();
    }

    void Update()
    {
        // - Upgrades
        if (_inUpgradePhase)
        {
            var upgradeCommand = _inputHandler.GetUpgradeCommand();

            if (upgradeCommand != null)
            {
                upgradeCommand.Execute();
                EndUpgradePhase();
            }
            
            return;
        }
        
        // - Player
        UpdatePlayer();
        
        // - Gun
        UpdateGun();
        
        // - ICommand
        _inputHandler.GetCommand()?.Execute();
        
        // - Gun - FSM Update
        _gunStateMachine.Update();

        // - Run Manager
        _runManager.TryStartRun();
        bool completed = _runManager.UpdateRun();
        
        if (completed)
            StartUpgradePhase();
        
        // - UI
        UpdateWeaponVisuals();
        UpdateUI();
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
        _targetManager = new TargetManager(targetTransforms);
    }

    void InitializeGuns()
    {
        // - Gun - FSM
        IWeapon rifle = new Rifle();
        IWeapon pistol = new Pistol();

        _gunStateMachine = new GunStateMachine(rifle, pistol, _player, _targetManager.GetTargets());
        _upgradeSystem = new UpgradeSystem(_gunStateMachine);
    }

    void InitializeSystems()
    {
        _runManager = new RunManager(playerTransform, _startZone, _startRadius, _targetManager);
        _uiManager = new UIManager(ammoText, upgradeText, runInfoText, bestRunTimeText);
        
        _inputHandler = new InputHandler(_gunStateMachine, _upgradeSystem);
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

    void UpdateUI()
    {
        _uiManager.UpdateAmmoUI(_gunStateMachine.CurrentWeapon.GetAmmo(), _gunStateMachine.CurrentWeapon.GetMaxAmmo());
        
        _uiManager.UpdateRunUI(_runManager.GetRunText(), _runManager.GetBestRunTime());
    }
    
    void StartUpgradePhase()
    {
        _inUpgradePhase = true;
        _uiManager.ShowUpgrade();
    }

    void EndUpgradePhase()
    {
        _inUpgradePhase = false;
        _uiManager.HideUpgrade();

        _targetManager.ResetTargets();
        
        _runManager.ResetRun();
    }
}