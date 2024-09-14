using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
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
        _attackBtn.HitEvent -= TakeDamage;
        _attackBtn.DeathEvent -= OnDeathEvent;
    }

    private void OnHitEvent()
    {
        StartCoroutine(PlayHitAnimWithDelay());
    }

    private void OnDeathEvent()
    {
        StartCoroutine(PlayHitAnimWithDelay());
    }

    private IEnumerator PlayHitAnimWithDelay()
    {
        yield return new WaitForSeconds(0.25f);
        TakeDamage();
    }

    private IEnumerator PlayDeathAnimWithDelay()
    {
        yield return new WaitForSeconds(0.25f);
        DeathAnimation();
    }

    private void TakeDamage()
    {
        _animator.SetTrigger("hit_1");
    }

    private void DeathAnimation()
    {
        _animator.SetTrigger("death");
    }
}
