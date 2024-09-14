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

    private float _delay = 0.5f;

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
        StartCoroutine(PlaySoundsWithDelay(_swordSound, _takeDamageSound, _delay));
    }

    private void OnDeathEvent()
    {
        StartCoroutine(PlaySoundsWithDelay(_swordSound, _deathSound, _delay));
    }

    private IEnumerator PlaySoundsWithDelay(AudioClip first, AudioClip second, float delay)
    {
        PlaySound(first);
        yield return new WaitForSeconds(delay);
        PlaySound(second);
    }

    private void PlaySound(AudioClip audioClip)
    {
        _audioSource?.PlayOneShot(audioClip);
    }
}
