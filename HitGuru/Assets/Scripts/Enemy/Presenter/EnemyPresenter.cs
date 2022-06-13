using UnityEngine;
using System;

public class EnemyPresenter : MonoBehaviour, IEnemyControl
{
    private EnemyModel _model;
    private EnemyTrigger _trigger;
    private Animator _animator;
    private Rigidbody _rigidbody;

    [SerializeField] private Transform _bodyTransform;
    [SerializeField] private EnemyView _view;
    [SerializeField] private EnemyEntity _enemyEntity;

    public event Action OnEnemyDefetead;

    private void Start()
    {
        _model = new EnemyModel(_enemyEntity);
        _trigger = GetComponent<EnemyTrigger>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();

        _trigger.OnTakeHit += TakeHitHandler;

        _view.UpdateView(GetEnemyViewModel(_model));
    }

    private void TakeHitHandler(Ammo ammo)
    {
        ApplyDamage();
        ammo.transform.parent = _bodyTransform;
    }

    private void ApplyDamage()
    {
        _model.TakeDamage(1);
        _view.UpdateView(GetEnemyViewModel(_model));
        if (_model.IsAlive == false)
        {
            _view.DisappearHealth();
            DeathEnemy();
        }
    }

    private EnemyViewModel GetEnemyViewModel(EnemyModel model)
    {
        var percentRemained = 1 - (_model._healthPoint / _model._maxHealthPoint);
        var healthPointsRemained = _model._healthPoint.ToString();
        return new EnemyViewModel(percentRemained, healthPointsRemained);
    }

    private void DeathEnemy()
    {
        OnEnemyDefetead?.Invoke();
        _animator.enabled = false;
        _rigidbody.isKinematic = true;
    }
}
