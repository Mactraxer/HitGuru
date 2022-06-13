public class EnemyModel
{
    public float _maxHealthPoint { get; private set; }
    public float _healthPoint { get; private set; }

    public bool IsAlive => _healthPoint > 0;

    public EnemyModel(EnemyEntity entity)
    {
        _healthPoint = entity.HealthPoints;
        _maxHealthPoint = entity.HealthPoints;
    }

    public void TakeDamage(int value)
    {
        _healthPoint -= value;
    }
}
