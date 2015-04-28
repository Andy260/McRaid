using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour 
{
    BearSoldier _soldier;    // Soldier to display health of
    Slider _healthBar;       // UI Silder used as Health display

    // Reference to actor which we will displaying health of
    public BearSoldier soldier
    {
        set
        {
            _soldier = value;

            // Update slider values
            _healthBar.maxValue = value._maxHealth;
            _healthBar.minValue = 0.0f;
        }
    }

    public void Awake()
    {
        _healthBar = GetComponent<Slider>();
    }

    void Update()
    {
        Vector3 newPosition = Camera.main.WorldToScreenPoint(
            _soldier.transform.position);

        // Update health bar position
        _healthBar.transform.position = newPosition;

        // Update health fill
        _healthBar.value = _soldier._currentHealth;
    }
}
