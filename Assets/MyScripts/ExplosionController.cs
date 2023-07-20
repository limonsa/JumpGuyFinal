using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private AudioSource _audio;
    // Start is called before the first frame update
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _audio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowExplosion() {
        _particleSystem.Play();
        //Debug.Log($"Va a sonar la explosion {_audio}");
        _audio.Play();
    }
}
