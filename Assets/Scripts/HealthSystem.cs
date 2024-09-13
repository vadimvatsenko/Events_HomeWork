using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int _currentHealth { get; private set; }
    private int _maxHealth => 100;

    private AttackBtn _attackBtn;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _attackBtn = FindAnyObjectByType<AttackBtn>();
    }
    private void OnEnable()
    {
        _attackBtn.OnButtonClickEvent += TakeDamage;
    }

    private void OnDisable()
    {
        _attackBtn.OnButtonClickEvent -= TakeDamage;
    }

    private int TakeDamage(int damage)
    {
        if(damage > _currentHealth)
        {
            _currentHealth = 0;
        } else
        {
            _currentHealth -= damage;
        }

        return _currentHealth;
    }

}
