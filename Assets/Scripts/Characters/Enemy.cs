using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyManager _enemyManager;

    // Use this for initialization
    void Start()
    {
        _enemyManager = EnemyManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < _enemyManager.VerticalDespawnThreshold)
        {
            Destroy(gameObject);
            --_enemyManager.EnemiesOnMap;
        }
    }
}
