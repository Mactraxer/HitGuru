using UnityEngine;

public class EnemyPresenter : MonoBehaviour
{
    private EnemyModel _model;
    private EnemyTrigger _trigger;
    private Animator _animator;
    private Rigidbody _rigidbody;
    [SerializeField] private Transform _bodyTransform;
    [SerializeField] private EnemyView _view;

    private void Start()
    {
        _model = new EnemyModel(2);//TODO remove to inspector. Use scriptable object
        _trigger = GetComponent<EnemyTrigger>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();

        _trigger.OnTakeHit += TakeHitHandler;
    }

    private void TakeHitHandler(Ammo ammo)
    {
        ApplyDamage();
        ammo.transform.parent = _bodyTransform;
    }

    private void ApplyDamage()
    {
        _model.TakeDamage(1);
        _view.DisplayHelthPercent(1 - (_model._healthPoint / _model._maxHealthPoint));//TODO remove to view model
        _view.DisplayHealthCount(_model._healthPoint.ToString());
        if (_model.IsAlive == false)
        {
            _view.DisappearHealth();
            DeathEnemy();
        }
    }

    private void DeathEnemy()
    {
        _animator.enabled = false;
        _rigidbody.isKinematic = true;
    }
}
