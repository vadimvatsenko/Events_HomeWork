using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private Image _healthBarCircleImage;
    private TextMeshProUGUI _percentText;
    private AttackBtn _attackBtn;

    private int _prefHealth = 100;

    private void Awake()
    {
        _attackBtn = FindAnyObjectByType<AttackBtn>();
        _healthBarCircleImage = this.transform.GetChild(0).GetComponent<Image>();
        _percentText = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _attackBtn.ChangeHealthBarEvent += ChangeHelthBarValue;
    }

    private void OnDisable()
    {
        _attackBtn.ChangeHealthBarEvent -= ChangeHelthBarValue;
    }
    private void ChangeHelthBarValue(int health)
    {
        StartCoroutine(SmoothChangeHelthBarValue(health));
    }

    private IEnumerator SmoothChangeHelthBarValue(int health)
    {
        float duration = 0.5f; // время за которое должна обновится полоска здоровья
        float elapsedTime = 0; // время которое прошло
        float startHeath = _prefHealth; // стартовое здоровье
        // test
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime; // за каждой итерации будем добавлять значение времени

            float percentageComplete = Mathf.Clamp01(elapsedTime / duration); // Mathf.Clamp01 ограничивает значение value между 0 и 1
            // elapsedTime / duration - Здесь мы вычисляем отношение текущего времени анимации (elapsedTime) к полной длительности анимации (duration).
            // Если, например, elapsedTime — это 0.25 секунд, а duration — это 1 секунда, то отношение будет равно 0.25, что означает, что 25 % анимации завершено.
            // Это отношение увеличивается с течением времени, от 0(в начале) до 1(когда анимация завершена).

            float currentHealth = Mathf.Lerp(startHeath, health, percentageComplete); 
            // percentageComplete может быть от 0 до 1, чем ближе значение, тем ближе currentHealth к health

            _healthBarCircleImage.fillAmount = currentHealth / 100f;

            _healthBarCircleImage.color = Color.Lerp(Color.red, Color.green, _healthBarCircleImage.fillAmount);

            if (_healthBarCircleImage.fillAmount <= 0)
            {
                _percentText.text = "Dead";
            }
            else
            {
                _percentText.text = (Mathf.RoundToInt(_healthBarCircleImage.fillAmount * 100)).ToString() + "%";
            }

            _percentText.color = _healthBarCircleImage.color;

            yield return null;
        }
        _prefHealth = health;
    }
}
