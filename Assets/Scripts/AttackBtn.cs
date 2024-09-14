using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class AttackBtn : MonoBehaviour, IPointerClickHandler
{
    public event Func<int, int> TakeDamageEvent;
    public event Action<int> ChangeHealthBarEvent;

    public event Action HitEvent;
    public event Action DeathEvent;
 
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
        _currentHealth = (int)TakeDamageEvent?.Invoke(_damage);

        if (_currentHealth <= 0)
        {
            DeathEvent?.Invoke();
        }
        else
        {
            HitEvent?.Invoke();
        }

        yield return new WaitForSeconds(0.75f);
        ChangeHealthBarEvent?.Invoke(_currentHealth);

        yield return null;
    }
}
