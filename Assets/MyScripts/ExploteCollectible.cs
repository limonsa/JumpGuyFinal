using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploteCollectible : MonoBehaviour
{
    private Animator _anim;
    private AudioSource _audio;
    private string _animName;
    private bool _explote;
    private float _explosionDelay;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        _animName = "explote";
        _explosionDelay = 1f;
        _anim.SetBool(_animName, false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _anim.SetBool(_animName, true);
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject, _explosionDelay);
        _audio.Play();
    }
}