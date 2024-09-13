using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class AttackBtn : MonoBehaviour, IPointerClickHandler
{
    public event Func<int, int> OnButtonClickEvent;
    public event Action<int> ChangeHealthBarEvent;
    public event Action HitEvent;
    public event Action DeathAnimationEvent;
    public event Action PlaySwardSoundEvent;
    public event Action PlayHitSoundEvent;
    public event Action PlayDeathSoundEvent;
    
    private int _damage;
    private int _currentHealth;
    public void OnPointerClick(PointerEventData eventData) 
    {
        //StartCoroutine(ClickButton());
    }

    public void OnClickHandler()
    {
        StartCoroutine(ClickButton());
    }

    private IEnumerator ClickButton()
    {
        _damage = Random.Range(1, 50);
        _currentHealth = (int)OnButtonClickEvent?.Invoke(_damage);

        if (_currentHealth <= 0)
        {
            PlaySwardSoundEvent?.Invoke();
            yield return new WaitForSeconds(0.25f);
            PlayDeathSoundEvent?.Invoke();
            DeathAnimationEvent?.Invoke();
        }
        else
        {
            PlaySwardSoundEvent?.Invoke();
            yield return new WaitForSeconds(0.25f);
            PlayHitSoundEvent?.Invoke();
            HitEvent?.Invoke();
        }

        yield return new WaitForSeconds(0.75f);
        ChangeHealthBarEvent?.Invoke(_currentHealth);

        yield return null;
    }
}
