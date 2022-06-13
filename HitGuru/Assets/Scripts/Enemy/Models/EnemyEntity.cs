using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Entity", fileName = "NewEnemyEntity")]
public class EnemyEntity: ScriptableObject
{
    [SerializeField] private int healthPoints;

    public int HealthPoints => healthPoints;
}
