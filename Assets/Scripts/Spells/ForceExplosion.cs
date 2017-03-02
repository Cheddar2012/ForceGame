using UnityEngine;

public class ForceExplosion : Spell
{
    [SerializeField]
    private float _explosionPullRadius = 20.0f;
    [SerializeField]
    private float _explosionPullDuration = 2.0f;
    [SerializeField]
    private float _explosionPullForce = 10.0f;

    [SerializeField]
    private float _explosionPushRadius = 10.0f;
    [SerializeField]
    private float _explosionPushDuration = 0.3f;
    [SerializeField]
    private float _explosionPushForce = 50.0f;
    
    // Use this for initialization
    protected override void OnStart()
    {
        _activities.Enqueue(new PointPull(transform, _explosionPullDuration, _explosionPullRadius, _explosionPullForce));
        _activities.Enqueue(new PointPush(transform, _explosionPushDuration, _explosionPushRadius, _explosionPushForce));
    }
}
