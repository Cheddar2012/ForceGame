using UnityEngine;

public class PointPull : SpellActivity
{
    private GameObject _pullParticleSystem;
    private float _radius;
    private float _force;

    private Collider[] affectedObjects;

    public PointPull(Transform anchor, float duration, float radius, float force)
    {
        _anchor = anchor;
        _duration = duration;
        _radius = radius;
        _force = force;

        // TODO: Create a ParticleManager class that allows us to spawn particle systems by passing in the name of our desired particle system, as well as the anchor.
        Object particleSystem = Resources.Load("ParticlesAbsorb");
        if (particleSystem)
        {
            _pullParticleSystem = (GameObject)Object.Instantiate(particleSystem, _anchor.position, _anchor.rotation);
            _pullParticleSystem.transform.parent = _anchor;
            _pullParticleSystem.SetActive(false);
        }
    }

    protected override void OnStart()
    {
        _pullParticleSystem.SetActive(true);

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
                Vector3 anchorPosRelativeToCol = _anchor.position - col.transform.position;
                anchorPosRelativeToCol.Normalize();
                Rigidbody rb = col.GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.AddForce(_force * anchorPosRelativeToCol);
                }
            }
        }
    }

    protected override void OnFinish()
    {
        Object.Destroy(_pullParticleSystem);
    }
}
