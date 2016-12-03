using UnityEngine;

public class ForceExplosion : Spell
{
    public GameObject pullParticleSystem;
    public float explosionPullRadius = 20.0f;
    public float explosionPullDuration = 2.0f;
    public float explosionPullForce = 10.0f;

    public GameObject pushParticleSystem;
    public float explosionPushRadius = 10.0f;
    public float explosionPushDuration = 0.3f;
    public float explosionPushForce = 50.0f;
    
    // Use this for initialization
    protected override void OnStart()
    {
        activities.Enqueue(new PointPull(transform, explosionPullDuration, explosionPullRadius, explosionPullForce));
        activities.Enqueue(new PointPush(transform, explosionPushDuration, explosionPushRadius, explosionPushForce));
    }
}
