using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(Thrower))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(AmmoSpawner))]
public class PlayerPresenter: MonoBehaviour // Drop MonoBehaviour and add DI 
{
    private PlayerAnimator _animator;
    private Thrower _thrower;
    private PlayerMover _mover;
    private AmmoSpawner _spawner;

    [SerializeField] private AmmoSpawnerSettings _spawnerSettings;

    private bool _throwLock = true;

    private void Start()
    {
        _animator = GetComponent<PlayerAnimator>();
        _thrower = GetComponent<Thrower>();
        _mover = GetComponent<PlayerMover>();
        _spawner = GetComponent<AmmoSpawner>();

        _thrower.OnThrow += PlayerThrowHandler;

        _mover.OnStartMove += PlayerStartMoveHandler;
        _mover.OnStopMove += PlayerStopMoveHandler;

        _animator.OnThrowAnimationEnd += ThrowAnimationEndHandler;
        _animator.OnThrowAnimationAction += ThrowAnimationActionHandler;

        _spawner.OnAmmoCollision += AmmoCollisionHandler;

        _spawner.Init(_spawnerSettings);
    }

    private void OnDisable()
    {
        _thrower.OnThrow -= PlayerThrowHandler;

        _mover.OnStartMove -= PlayerStartMoveHandler;
        _mover.OnStopMove -= PlayerStopMoveHandler;

        _animator.OnThrowAnimationEnd -= ThrowAnimationEndHandler;
        _animator.OnThrowAnimationAction -= ThrowAnimationActionHandler;

        _spawner.OnAmmoCollision -= AmmoCollisionHandler;
    }

    public void Hit()
    {
        if (_throwLock == true) return;
        
        _throwLock = true;
        _spawner.SpawnAmmo();
        _thrower.CalculateTrajectory();
    }

    public void Move()
    {
        _throwLock = true;
        _mover.MoveNextPoint();
    }

    // Thrower action handler
    private void PlayerThrowHandler()
    {
        _animator.AnimateThrow();
    }

    // Animator actoin handlers
    private void ThrowAnimationActionHandler()
    {
        GameObject ammo = _spawner.GetLastSpawnedAmmo();
        ammo.transform.rotation = Quaternion.Euler(0, 0, 0);
        ammo.transform.parent = null;

        _thrower.ThrowAmmo(ammo);   
    }

    private void ThrowAnimationEndHandler()
    {
        _throwLock = false;
    }

    private void PlayerStartMoveHandler()
    {
        _animator.AnimateRun();
    }

    private void PlayerStopMoveHandler()
    {
        _animator.AnimateIdle();
        _throwLock = false;
    }

    private void AmmoCollisionHandler()
    {
        _thrower.StopThrowing();
    }
}
