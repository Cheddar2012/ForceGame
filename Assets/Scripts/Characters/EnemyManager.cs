
public class EnemyManager
{
    public int EnemiesOnMap;
    public float VerticalDespawnThreshold { get; private set; }

    private static EnemyManager _instance;
    public static EnemyManager Instance
    {
        get
        {
            return _instance ?? (_instance = new EnemyManager());
        }
    }

    private EnemyManager()
    {
        EnemiesOnMap = 0;
        VerticalDespawnThreshold = -10;
    }
}
