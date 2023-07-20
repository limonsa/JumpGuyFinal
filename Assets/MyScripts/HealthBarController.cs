using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    private Image _healthBar;
    private ParticleSystem _particles;
    private float _healthPercentage;
    private float _healthAmount;
    private float _maxHealth;

    // Start is called before the first frame update
    void Start() {
        _healthBar = GameObject.Find("Bar").GetComponent<Image>();
        _particles = GameObject.Find("Particles").GetComponent<ParticleSystem>();
        _maxHealth = 100f;
        _healthAmount = _maxHealth;
        _healthPercentage = _maxHealth / 100f;
    }

    // Update is called once per frame
    void Update() {
    }

    /// <summary>
    /// Updates the health Bar with the damage taken
    /// </summary>
    /// <param name="damagePercentage"></param>
    /// <returns>bool: False if the damage caused dead or True if player is alive</returns>
    public bool TakeDamage(float damagePercentage) {
        bool isDead = false;
        _particles.Play();
        _healthPercentage -= damagePercentage;
        if (_healthPercentage <= 0f)
        {
            _healthPercentage = 0f;
            _healthAmount = 0f;
            isDead = true;
        }else {
            _healthAmount -= _maxHealth * damagePercentage;
        }
        _healthBar.fillAmount = _healthPercentage;
        return isDead;
    }

    public void Heal(float healingPercentage) {
        _healthPercentage += healingPercentage;
        if (_healthPercentage > 1f) {
            _healthPercentage = 1f;
            _healthAmount = _maxHealth;
        }
        else {
            _healthAmount += _maxHealth * healingPercentage;
        }
        _healthBar.fillAmount = _healthPercentage;
        
    }
}
