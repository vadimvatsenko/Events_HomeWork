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
        _attackBtn.HitAnimationEvent += TakeDamage;
        _attackBtn.DeathAnimationEvent += DeathAnimation;
    }

    private void OnDisable()
    {
        _attackBtn.HitAnimationEvent -= TakeDamage;
        _attackBtn.DeathAnimationEvent -= DeathAnimation;
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
