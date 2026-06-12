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
    
    [Header("Startzone")]
    [SerializeField] private Transform _startZone;
    [SerializeField] private float _startRadius = 2f;

    [Header("UI")] 
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private TMP_Text upgradeText;
    [SerializeField] private TMP_Text runInfoText;
    
    // - Input
    private InputHandler _inputHandler;
    
    // - Target
    private List<TargetActor> _targets;
    
    // - Timer / Run
    private float _runTimer;
    private enum RunState{Running, Completed, WaitingToStart}
    private RunState _runState;
    private int _runCount;
    
    // - Upgrade
    private UpgradeSystem _upgradeSystem;
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
        
        // - Input
        _inputHandler = new InputHandler(_gunStateMachine, _upgradeSystem);
        
        // - Timer
        _runState = RunState.WaitingToStart;
        _runTimer = 0;
    }

    void Update()
    {
        // - Upgrades
        if (_inUpgradePhase)
        {
            ICommand upgradeCommand = _inputHandler.GetUpgradeCommand();

            if (upgradeCommand != null)
            {
                upgradeCommand.Execute();
                EndUpgradePhase();
            }
            
            return;
        }
        
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
        
        // - Run
        StartRun();
        UpdateRun();
        UpdateRunUI();
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
        _upgradeSystem = new UpgradeSystem(_gunStateMachine);
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

    void UpdateAmmoUI()
    {
        ammoText.text = _gunStateMachine.CurrentWeapon.GetAmmo() + " / " + _gunStateMachine.CurrentWeapon.GetMaxAmmo();
    }

    void StartRun()
    {
        if (_runState != RunState.WaitingToStart)
            return;
        
        if (!IsPlayerAtStart())
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            _runState = RunState.Running;
            _runTimer = 0;
        }
    }

    void UpdateRun()
    {
        if (_runState != RunState.Running)
            return;

        _runTimer += Time.deltaTime;
        
        if (AreAllTargetsDestroyed())
            CompleteRun();
    }

    void CompleteRun()
    {
        _runState = RunState.Completed;
        _runCount++;
        
        Debug.Log("Time: " + _runTimer.ToString("F2"));

        StartUpgradePhase();
    }

    void ResetRun()
    {
        _runTimer = 0;
        _runState = RunState.WaitingToStart;
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

    void ResetTargets()
    {
        foreach (TargetActor target in _targets)
            target.Reset();
    }

    void StartUpgradePhase()
    {
        _inUpgradePhase = true;
        
        upgradeText.gameObject.SetActive(true);
    }

    void EndUpgradePhase()
    {
        _inUpgradePhase = false;
        
        upgradeText.gameObject.SetActive(false);
        
        ResetTargets();
        ResetRun();
    }

    bool IsPlayerAtStart()
    {
        return Vector3.Distance(playerTransform.position, _startZone.position) < _startRadius;
    }

    void UpdateRunUI()
    {
        if (_runState == RunState.WaitingToStart)
        {
            if (IsPlayerAtStart())
                runInfoText.text = "Press E to start run";
            else
                runInfoText.text = "Go to Start zone!!";
        }
        else if (_runState == RunState.Running)
        {
            runInfoText.text = "Run: " + (_runCount + 1) +"\nTime: " + _runTimer.ToString("F2");
        }
        else if (_runState == RunState.Completed)
        {
            runInfoText.text = "Complete!\nTime: " + _runTimer.ToString("F2");
        }
    }
}