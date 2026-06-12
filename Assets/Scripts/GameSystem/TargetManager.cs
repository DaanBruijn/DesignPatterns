using System.Collections.Generic;
using UnityEngine;

// - Script that handles the targets for the game
// - Daniel Bruijn

public class TargetManager
{
    // - Variables
    private List<TargetActor> _targets;

    public TargetManager(Transform[] targetTransforms)
    {
        _targets = new List<TargetActor>();

        foreach (var target in targetTransforms)
        {
            _targets.Add(new TargetActor(target, new Target(100)));
        }
    }

    public List<TargetActor> GetTargets()
    {
        return _targets;
    }
    
    public bool AreAllTargetsDestroyed()
    {
        foreach (TargetActor target in _targets)
            if (!target.TargetData.IsDestroyed)
                return false;

        return true;
    }
    
    public void ResetTargets()
    {
        foreach (TargetActor target in _targets)
            target.Reset();
    }
}
