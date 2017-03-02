using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // The enemy to spawn
    [SerializeField]
    private GameObject _enemy;

    // The radius within which enemies will be spawned
    [SerializeField]
    private float _radius = 20.0f;

    // Timer elapsed before a new enemy is spawned, in seconds
    [SerializeField]
    private float _spawnTimer = 3.0f;

    [SerializeField]
    private int _maxEnemiesOnMap = 20;

    private float _timer;
    private Transform _player;

    private EnemyManager _enemyManager;

    // Use this for initialization
    void Start()
    {
        _timer = 0;
        _player = GameObject.FindWithTag("Player").transform;
        _enemyManager = EnemyManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyManager.Instance.EnemiesOnMap < _maxEnemiesOnMap)
        {
            _timer += Time.deltaTime;

            if (_timer > _spawnTimer)
            {
                if (_enemy)
                {
                    InstantiateEnemy();
                }
                _timer = 0;
            }
        }
    }

    void InstantiateEnemy()
    {
        Vector3 position = GetSpawnPosition();
        Quaternion rotation = GetSpawnRotation(position);
        Instantiate(_enemy, position, rotation);
        ++_enemyManager.EnemiesOnMap;
    }

    // Get a random spawn position within the radius of the spawner
    Vector3 GetSpawnPosition()
    {
        // Get the offset fromn the spawner as a point within the spawn circle
        Vector3 offsetFromOrigin = _radius * (Vector3)Random.insideUnitCircle;

        // Use z axis instead of y, since we want to spawn on the xz plane
        offsetFromOrigin.z = offsetFromOrigin.y;
        offsetFromOrigin.y = 0;

        // Add the offset to the spawner's position to get the spawn location
        Vector3 spawnPosition = transform.position + offsetFromOrigin;

        return spawnPosition;
    }

    // Get the rotation of the enemy so that the enemy is looking at the player
    Quaternion GetSpawnRotation(Vector3 spawnPosition)
    {
        // Get the player's position relative to the enemy spawn location
        Vector3 relativePosition = _player.position - spawnPosition;

        // Ignore the difference in the player's y value and the enemy's
        // We do not want the enemy looking up or down by default
        relativePosition.y = spawnPosition.y;

        Quaternion spawnRotation = Quaternion.LookRotation(relativePosition);

        return spawnRotation;
    }
}
