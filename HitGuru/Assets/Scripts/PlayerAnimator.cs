using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    // ===== Clips name ===== //
    private const string RUN_CLIP_NAME = "Run";
    private const string IDLE_CLIP_NAME = "Idle";
    private const string THROW_CLIP_NAME = "THROW";

    private Animator _animator;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        AnimateIdle();
    }

    public void AnimateIdle()
    {
        _animator.Play(IDLE_CLIP_NAME);
    }

    public void AnimateThrow()
    {
        _animator.Play(THROW_CLIP_NAME);
    }

    public void AnimateRun()
    {
        _animator.Play(RUN_CLIP_NAME);
    }
}
