using UnityEngine;

[CreateAssetMenu(menuName = "Game level", fileName = "new Game level")]
public class GameLevelEntity: ScriptableObject
{
    [SerializeField] private int _enemyCount;

    public int EnemyCount => _enemyCount;

    public GameLevelEntity(int enemyCount)
    {
        _enemyCount = enemyCount;
    }
}
