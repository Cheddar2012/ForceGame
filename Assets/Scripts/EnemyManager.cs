
public class EnemyManager
{
    public int enemiesOnMap;
    public float verticalDespawnThreshold { get; private set; }

    private static EnemyManager instance;
    public static EnemyManager Instance
    {
        get
        {
            return instance ?? (instance = new EnemyManager());
        }
    }

    private EnemyManager()
    {
        enemiesOnMap = 0;
        verticalDespawnThreshold = -10;
    }
}
