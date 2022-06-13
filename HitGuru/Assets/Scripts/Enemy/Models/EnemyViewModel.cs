public class EnemyViewModel
{
    public float _healthPercentRemained { get; private set; }
    public string _healthCountRemained { get; private set; }

    public EnemyViewModel(float healthPercentRemained, string healthCountRemained)
    {
        _healthPercentRemained = healthPercentRemained;
        _healthCountRemained = healthCountRemained;
    }
}
