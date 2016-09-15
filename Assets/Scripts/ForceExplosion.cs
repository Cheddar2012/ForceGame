using UnityEngine;

public class ForceExplosion : MonoBehaviour
{
    public GameObject pullParticleSystem;
    public float explosionPullRadius = 10.0f;
    public float explosionPullDuration = 2.0f;
    public float explosionPullForce = 10.0f;

    public GameObject pushParticleSystem;
    public float explosionPushRadius = 10.0f;
    public float explosionPushDuration = 0.3f;
    public float explosionPushForce = 50.0f;

    private float explosionPullCountDownTimer = 0.0f;
    private Collider[] affectedObjects;

    private SpellManager spellManager;

    // Use this for initialization
    void Start()
    {
        spellManager = SpellManager.Instance;

        pullParticleSystem = (GameObject)Instantiate(Resources.Load("ParticlesAbsorb"), transform.position, transform.rotation);
        pushParticleSystem = (GameObject)Instantiate(Resources.Load("ParticlesExplosion"), transform.position, transform.rotation);
        pullParticleSystem.transform.parent = transform;
        pushParticleSystem.transform.parent = transform;
        pullParticleSystem.SetActive(true);
        pushParticleSystem.SetActive(false);

        BeginForceExplosion();
    }

    // Update is called once per frame
    void Update()
    {
        explosionPullCountDownTimer -= Time.deltaTime;

        if (explosionPullCountDownTimer <= 0.0f)
        {
            FinishForceExplosion();
        }
        else
        {
            affectedObjects = Physics.OverlapSphere(transform.position, explosionPullRadius);
        }
    }

    void FixedUpdate()
    {
        if (explosionPullCountDownTimer > 0.0f)
        {
            ForcePull();
        }
    }

    void BeginForceExplosion()
    {
        affectedObjects = Physics.OverlapSphere(transform.position, explosionPullRadius);
        explosionPullCountDownTimer = explosionPullDuration;
    }

    void FinishForceExplosion()
    {
        pullParticleSystem.SetActive(false);
        ForcePush();
        explosionPullCountDownTimer = 0;
    }

    void ForcePull()
    {
        foreach (Collider col in affectedObjects)
        {
            if (col.tag != "Player")
            {
                Vector3 relativePosition = transform.position - col.transform.position;
                relativePosition.Normalize();
                Rigidbody rb = col.GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.AddForce(explosionPullForce * relativePosition);
                }
            }
        }
    }

    void ForcePush()
    {
        affectedObjects = Physics.OverlapSphere(transform.position, explosionPushRadius);
        foreach (Collider col in affectedObjects)
        {
            Vector3 relativePosition = col.transform.position - transform.position;
            relativePosition.Normalize();
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.AddForce(explosionPushForce * relativePosition);
            }
        }

        pushParticleSystem.SetActive(true);
        Invoke("FinishCasting", explosionPushDuration);
    }

    void FinishCasting()
    {
        Destroy(gameObject);
        spellManager.FinishSpellCast();
    }
}
