using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    // ===== Clips name ===== //
    private const string RUN_CLIP_NAME = "Run";
    private const string IDLE_CLIP_NAME = "Idle";
    private const string THROW_CLIP_NAME = "Throw";

    private Animator _animator;
    public event Action ThrowAnimationEnd;

    public bool ThrowAnimated => _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != THROW_CLIP_NAME;
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

    public void AnimationEnd()
    {
        ThrowAnimationEnd?.Invoke();
    }
}
