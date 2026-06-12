using UnityEngine;

// - Actor script for Target References
// - Daniel Bruijn

public class TargetActor
{
    // - Variables
    public Transform Transform { get; }
    
    public Target TargetData { get; }

    public TargetActor(Transform transform, Target target)
    {
        Transform = transform;
        TargetData = target;
    }
}
