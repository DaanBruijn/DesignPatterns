using UnityEngine;

// - System used to handle all the Runs
// - Daniel Bruijn

public class RunManager
{
    // - Variables
    private enum RunState{WaitingToStart, Running, Completed}
    
    private float _runTimer;
    private int _runCount;
    private RunState _runState;
    private float _bestRunTime;

    private Transform _player;
    private Transform _startZone;
    private float _startRadius;
    private TargetManager _targetManager;

    public RunManager(Transform player, Transform startZone, float startRadius, TargetManager targetManager)
    {
        _player = player;
        _startZone = startZone;
        _startRadius = startRadius;
        _targetManager = targetManager;
        
        _runState = RunState.WaitingToStart;
    }
    
    public void TryStartRun()
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
    
    public bool UpdateRun()
    {
        if (_runState != RunState.Running)
            return false;

        _runTimer += Time.deltaTime;

        if (_targetManager.AreAllTargetsDestroyed())
        {
            CompleteRun();
            return true;
        }

        return false;
    }

    public void CompleteRun()
    {
        _runState = RunState.Completed;
        _runCount++;
        
        if (_bestRunTime < _runTimer)
            _bestRunTime = _runTimer;
        
        Debug.Log("Time: " + _runTimer.ToString("F2"));
    }

    public void ResetRun()
    {
        _runTimer = 0;
        _runState = RunState.WaitingToStart;
    }
    
    public bool IsPlayerAtStart()
    {
        return Vector3.Distance(_player.position, _startZone.position) < _startRadius;
    }

    public string GetRunText()
    {
        if (_runState == RunState.WaitingToStart)
        {
            if (IsPlayerAtStart())
                return "Press E to start run";
            else
                return "Go to Start zone!!";
        }

        if (_runState == RunState.Running)
            return $"Run {_runCount + 1}\nTime: {_runTimer:F2}";

        if (_runState == RunState.Completed)
            return $"Complete!\nTime: {_runTimer:F2}";

        return "";
    }

    public float GetBestRunTime()
    {
        return _bestRunTime;
    }
}
