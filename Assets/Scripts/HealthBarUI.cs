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
        float duration = 0.5f; // ����� �� ������� ������ ��������� ������� ��������
        float elapsedTime = 0; // ����� ������� ������
        float startHeath = _prefHealth; // ��������� ��������
        // test
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime; // �� ������ �������� ����� ��������� �������� �������

            float percentageComplete = Mathf.Clamp01(elapsedTime / duration); // Mathf.Clamp01 ������������ �������� value ����� 0 � 1
            // elapsedTime / duration - ����� �� ��������� ��������� �������� ������� �������� (elapsedTime) � ������ ������������ �������� (duration).
            // ����, ��������, elapsedTime � ��� 0.25 ������, � duration � ��� 1 �������, �� ��������� ����� ����� 0.25, ��� ��������, ��� 25 % �������� ���������.
            // ��� ��������� ������������� � �������� �������, �� 0(� ������) �� 1(����� �������� ���������).

            float currentHealth = Mathf.Lerp(startHeath, health, percentageComplete); 
            // percentageComplete ����� ���� �� 0 �� 1, ��� ����� ��������, ��� ����� currentHealth � health

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
