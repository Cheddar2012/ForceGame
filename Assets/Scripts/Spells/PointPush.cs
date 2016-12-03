using UnityEngine;

public class PointPush : SpellActivity
{
    private GameObject _pushParticleSystem;
    private float _radius;
    private float _force;

    private Collider[] affectedObjects;

    public PointPush(Transform anchor, float duration, float radius, float force)
    {
        _anchor = anchor;
        _duration = duration;
        _radius = radius;
        _force = force;

        // TODO: Create a ParticleManager class that allows us to spawn particle systems by passing in the name of our desired particle system, as well as the anchor.
        Object particleSystem = Resources.Load("ParticlesExplosion");
        if (particleSystem)
        {
            _pushParticleSystem = (GameObject)Object.Instantiate(particleSystem, _anchor.position, _anchor.rotation);
            _pushParticleSystem.transform.parent = _anchor;
            _pushParticleSystem.SetActive(false);
        }
    }

    protected override void OnStart()
    {
        _pushParticleSystem.SetActive(true);

        affectedObjects = Physics.OverlapSphere(_anchor.position, _radius);
    }

    protected override void OnUpdate()
    {
        affectedObjects = Physics.OverlapSphere(_anchor.position, _radius);
    }

    protected override void OnFixedUpdate()
    {
        foreach (Collider col in affectedObjects)
        {
            if (col.tag != "Player")
            {
                Vector3 colPosRelativeToAnchor = col.transform.position - _anchor.position;
                colPosRelativeToAnchor.Normalize();
                Rigidbody rb = col.GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.AddForce(_force * colPosRelativeToAnchor);
                }
            }
        }
    }

    protected override void OnFinish()
    {
        Object.Destroy(_pushParticleSystem);
    }
}
