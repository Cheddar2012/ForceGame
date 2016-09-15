using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // The enemy to spawn
    public GameObject enemy;

    // The radius within which enemies will be spawned
    public float radius = 20.0f;

    // Timer elapsed before a new enemy is spawned, in seconds
    public float spawnTimer = 3.0f;

    public int maxEnemiesOnMap = 20;

    private float timer;
    private Transform player;

    private EnemyManager enemyManager;

    // Use this for initialization
    void Start()
    {
        timer = 0;
        player = GameObject.FindWithTag("Player").transform;
        enemyManager = EnemyManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyManager.Instance.enemiesOnMap < maxEnemiesOnMap)
        {
            timer += Time.deltaTime;

            if (timer > spawnTimer)
            {
                if (enemy)
                {
                    InstantiateEnemy();
                }
                timer = 0;
            }
        }
    }

    void InstantiateEnemy()
    {
        Vector3 position = getSpawnPosition();
        Quaternion rotation = getSpawnRotation(position);
        Instantiate(enemy, position, rotation);
        ++enemyManager.enemiesOnMap;
    }

    // Get a random spawn position within the radius of the spawner
    Vector3 getSpawnPosition()
    {
        // Get the offset fromn the spawner as a point within the spawn circle
        Vector3 offsetFromOrigin = radius * (Vector3)Random.insideUnitCircle;

        // Use z axis instead of y, since we want to spawn on the xz plane
        offsetFromOrigin.z = offsetFromOrigin.y;
        offsetFromOrigin.y = 0;

        // Add the offset to the spawner's position to get the spawn location
        Vector3 spawnPosition = transform.position + offsetFromOrigin;

        return spawnPosition;
    }

    // Get the rotation of the enemy so that the enemy is looking at the player
    Quaternion getSpawnRotation(Vector3 spawnPosition)
    {
        // Get the player's position relative to the enemy spawn location
        Vector3 relativePosition = player.position - spawnPosition;

        // Ignore the difference in the player's y value and the enemy's
        // We do not want the enemy looking up or down by default
        relativePosition.y = spawnPosition.y;

        Quaternion spawnRotation = Quaternion.LookRotation(relativePosition);

        return spawnRotation;
    }
}
