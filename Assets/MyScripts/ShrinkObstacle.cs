using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkObstacle : MonoBehaviour
{
    private Animator _collectibleAnim;
    private GameObject _obstacle;
    private ParticleSystem _particles;
    private string _collectibleAnimName;

    // Start is called before the first frame update
    void Start()
    {
        _obstacle = GameObject.Find("Obstacle");
        _collectibleAnim = GetComponent<Animator>();
        _collectibleAnimName = "floating";
        _particles = GameObject.Find("ParticleSystem").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _obstacle.transform.localScale -= new Vector3(0.4f, 1f, 0);
        _collectibleAnim.SetBool(_collectibleAnimName, true);
        _particles.transform.position = _obstacle.transform.position;
        //_particles.Simulate(_exposionDelay, true, true, false);
        _particles.Play();
    }
}