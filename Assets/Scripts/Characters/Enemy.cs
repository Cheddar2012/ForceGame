using UnityEngine;

public class Enemy : MonoBehaviour
{
    EnemyManager enemyManager;

    // Use this for initialization
    void Start()
    {
        enemyManager = EnemyManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < enemyManager.verticalDespawnThreshold)
        {
            Destroy(gameObject);
            --enemyManager.enemiesOnMap;
        }
    }
}
