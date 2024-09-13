using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AttackBtn _attackBtn;
    private AudioSource _audioSource;
    private AudioClip _swordSound;
    private AudioClip _takeDamageSound;
    private AudioClip _deathSound;

    private void Awake()
    {
        _attackBtn  = FindAnyObjectByType<AttackBtn>();
    }
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _swordSound = Resources.Load<AudioClip>("Sounds/SwordSound");
        _takeDamageSound = Resources.Load<AudioClip>("Sounds/HitSound");
        _deathSound = Resources.Load<AudioClip>("Sounds/DeathSound");
    }

    private void OnEnable()
    {
        _attackBtn.PlaySwardSoundEvent += PlaySwardSound;
        _attackBtn.PlayHitSoundEvent += PlayTakeHitSound;
        _attackBtn.PlayDeathSoundEvent += PlayDeathSound;
    }

    private void OnDisable()
    {
        _attackBtn.PlaySwardSoundEvent -= PlaySwardSound;
        _attackBtn.HitAnimationEvent -= PlayTakeHitSound;
        _attackBtn.PlayDeathSoundEvent -= PlayDeathSound;
    }

    private void PlaySwardSound()
    {
        _audioSource?.PlayOneShot(_swordSound);
    }

    private void PlayTakeHitSound()
    {
        _audioSource?.PlayOneShot(_takeDamageSound);
    }

    private void PlayDeathSound()
    {
        _audioSource?.PlayOneShot(_deathSound);
    }
}
