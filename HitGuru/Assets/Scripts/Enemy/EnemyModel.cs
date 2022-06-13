public class EnemyModel
{
    public float _maxHealthPoint { get; private set; }
    public float _healthPoint { get; private set; }

    public bool IsAlive => _healthPoint > 0;

    public EnemyModel(int maxHealthPoint)
    {
        _healthPoint = maxHealthPoint;
        _maxHealthPoint = maxHealthPoint;
    }

    public void TakeDamage(int value)
    {
        _healthPoint -= value;
    }
}
