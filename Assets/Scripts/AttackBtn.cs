using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class AttackBtn : MonoBehaviour, IPointerClickHandler
{
    private float _width;
    private float _height;
    private RectTransform _rectTransform;

    public event Func<int, int> TakeDamageEvent;
    public event Action<int> ChangeHealthBarEvent;

    public event Action HitEvent;
    public event Action DeathEvent;
 
    private int _damage;
    private int _currentHealth;

    private void Awake()
    {
        _width = Screen.width;
        _height = Screen.height * 0.1f;

        _rectTransform = GetComponent<RectTransform>();

        //_rectTransform.sizeDelta = new Vector2(_width, _height);

        /*_rectTransform.anchorMin = new Vector2(0, 0);
        _rectTransform.anchorMax = new Vector2(1, 0.1f);*/
        

    }
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
