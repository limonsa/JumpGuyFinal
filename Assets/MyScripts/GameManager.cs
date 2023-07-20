using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public enum Surrounding
    {
        isDay,
        isNight,
        isSpace,
    }
    private Surrounding _currentSurrounding;
    private TextMeshPro _banner;
    private Animator _animBeetle;
    private HealthBarController _healthBar;
    private Light2D _background;
    private GameObject _backgroundNight;
    private GameObject _backgroundSpace;
    private AudioSource _audio;

    // Start is called before the first frame update
    void Start()
    {
        _currentSurrounding = Surrounding.isDay;
        _banner = GameObject.Find("Banner").GetComponent<TextMeshPro>();
        _banner.enabled = false;
        _animBeetle = GameObject.Find("Enemy").GetComponent<Animator>();
        _healthBar = GameObject.Find("HealthBar").GetComponent<HealthBarController>();
        _background = GameObject.Find("Day").GetComponent<Light2D>();
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetCurrentSurrounding() {
        return _currentSurrounding.ToString();
    }

    public void ChangeBackground() {
        switch (_currentSurrounding)
        {
            case Surrounding.isDay:
                _currentSurrounding = Surrounding.isNight;
                //Each color value (float) r, g, b, a must be divided by 255 as Color constructor expects a percentage on each value
                _background.color = new Color(193f / 255f, 158f / 255f, 182f / 255f, 255f / 255f);
                _background.intensity = 0.3f;
                break;
            case Surrounding.isNight:
                _currentSurrounding = Surrounding.isSpace;
                _background.color = new Color(94f / 255f, 120f / 255f, 176f / 255f, 255f / 255f);
                _background.intensity = 1.8f;
                break;
            case Surrounding.isSpace:
                _currentSurrounding = Surrounding.isDay;
                _background.color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
                _background.intensity = 1f;
                break;
        }
    }

    public void EndGame() {
        _audio.Stop();
        _animBeetle.SetTrigger("isDead");
        _banner.enabled = true;
    }

    /// <summary>
    /// Applies the damage to the Health Bar
    /// </summary>
    /// <param name="damagePercentage"></param>
    /// <returns>False if the damage killed the player, true if the player could take the damage without dying</returns>
    public bool TakeDamage(float damagePercentage) {
        bool isAlive = true;
        if (_healthBar.TakeDamage(damagePercentage)) {
            EndGame();
            isAlive = false;
        }
        return isAlive;
    }
}
