using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackground : MonoBehaviour
{
    private Animator _anim;
    private GameManager _gm;
    private string _background;

    // Start is called before the first frame update
    void Start()
    {
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        _anim = GameObject.Find("Background").GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string temp;
        _gm.ChangeBackground();
        temp = _gm.GetCurrentSurrounding();
        _anim.SetTrigger(temp); ;
    }
}
