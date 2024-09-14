using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public enum AnimName
    {
        hit_1,
        death
    }

    private float _delay = 0.5f;
    private Animator _animator;
    private AttackBtn _attackBtn;

    private void Awake()
    {
        _attackBtn = FindAnyObjectByType<AttackBtn>();
    }
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _attackBtn.HitEvent += OnHitEvent;
        _attackBtn.DeathEvent += OnDeathEvent;
    }

    private void OnDisable()
    {
        _attackBtn.HitEvent -= OnHitEvent;
        _attackBtn.DeathEvent -= OnDeathEvent;
    }

    private void OnHitEvent()
    {
        StartCoroutine(PlayAnimWithDelay("hit_1", _delay));
    }

    private void OnDeathEvent()
    {
        StartCoroutine(PlayAnimWithDelay("death", _delay));
    }

    private IEnumerator PlayAnimWithDelay(string animName, float delay)
    {
        yield return new WaitForSeconds(0.25f);
        PlayAnim(animName);
    }

    private void PlayAnim(string animName)
    {
        _animator.SetTrigger(animName);
    }
}
