public class GameLevelModel 
{
    private int _enemyCount;

    public int EnemyCount => _enemyCount;

    public GameLevelModel(GameLevelEntity entity)
    {
        _enemyCount = entity.EnemyCount;
    }

    public void EnemyDefeated()
    {
        _enemyCount -= 1;
    }
}
