using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(Thrower))]
[RequireComponent(typeof(PlayerMover))]
public class PlayerPresenter: MonoBehaviour // Drop MonoBehaviour and add DI 
{
    private PlayerAnimator _animator;
    private Thrower _thrower;
    private PlayerMover _mover;

    private void Start()
    {
        _animator = GetComponent<PlayerAnimator>();
        _thrower = GetComponent<Thrower>();
        _mover = GetComponent<PlayerMover>();

        _thrower.OnThrow += PlayerThrowHandler;
        _mover.OnStartMove += PlayerStartMoveHandler;
        _mover.OnStopMove += PlayerStopMoveHandler;
    }

    private void OnDisable()
    {
        _thrower.OnThrow -= PlayerThrowHandler;
        _mover.OnStartMove -= PlayerStartMoveHandler;
        _mover.OnStopMove -= PlayerStopMoveHandler;
    }

    private void PlayerStartMoveHandler()
    {
        _animator.AnimateRun();
    }
    
    private void PlayerThrowHandler()
    {
        _animator.AnimateThrow();
    }

    private void PlayerStopMoveHandler()
    {
        _animator.AnimateIdle();
    }
}
