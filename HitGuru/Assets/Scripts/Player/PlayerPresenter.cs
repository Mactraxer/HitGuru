using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(Thrower))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(AmmoSpawner))]
public class PlayerPresenter: MonoBehaviour // Drop MonoBehaviour and add DI 
{
    [SerializeField] private Ammo _ammoPrefab;
    [SerializeField] private Transform _spawnTransform;

    private PlayerAnimator _animator;
    private Thrower _thrower;
    private PlayerMover _mover;
    private AmmoSpawner _spawner;
    private PlayerInput _input;

    private void Start()
    {
        _animator = GetComponent<PlayerAnimator>();
        _thrower = GetComponent<Thrower>();
        _mover = GetComponent<PlayerMover>();
        _spawner = GetComponent<AmmoSpawner>();
        _input = GetComponent<PlayerInput>();

        _spawner.OnDetectEmeny += DetectEnemyHandler;
        _spawner.OnDetectObstacle += DetectObstacleHandler;

        _thrower.OnThrow += PlayerThrowHandler;

        _mover.OnStartMove += PlayerStartMoveHandler;
        _mover.OnStopMove += PlayerStopMoveHandler;

        _animator.ThrowAnimationEnd += ThrowAnimationEndHandler;

        _input.OnInputHit += InputHitHandler;
        _input.OnInputMove += InputMoveHandler;

        _spawner.Init(3, _ammoPrefab, _spawnTransform);
    }

    private void OnDisable()
    {
        _spawner.OnDetectEmeny -= DetectEnemyHandler;
        _spawner.OnDetectObstacle -= DetectObstacleHandler;

        _thrower.OnThrow -= PlayerThrowHandler;

        _mover.OnStartMove -= PlayerStartMoveHandler;
        _mover.OnStopMove -= PlayerStopMoveHandler;

        _animator.ThrowAnimationEnd -= ThrowAnimationEndHandler;

        _input.OnInputHit -= InputHitHandler;
        _input.OnInputMove -= InputMoveHandler;
    }

    // Thrower action handler
    private void PlayerThrowHandler()
    {
        _animator.AnimateThrow();
    }

    // Animator actoin handlers
    private void ThrowAnimationEndHandler()
    {
        Ammo ammo = _spawner.GetLastSpawnedAmmo();
        ammo.transform.rotation = Quaternion.Euler(0,0,0);
        ammo.transform.parent = null;
        
        _thrower.ThrowAmmo(ammo);
    }

    private void PlayerStartMoveHandler()
    {
        _animator.AnimateRun();
    }

    private void PlayerStopMoveHandler()
    {
        _animator.AnimateIdle();
    }

    // Spawner action handlers
    private void DetectEnemyHandler(Ammo ammo)
    {
        _spawner.StopSimulateAmmo(ammo);
    }

    private void DetectObstacleHandler(Ammo ammo)
    {
        _spawner.StopSimulateAmmo(ammo);
    }

    // Input action handlers
    private void InputMoveHandler()
    {
        _mover.MoveNextPoint();
    }

    private void InputHitHandler()
    {
        if (_animator.ThrowAnimated == false) return;
        
        _spawner.SpawnAmmo();
        _thrower.CalculateTrajectory();
    }
}