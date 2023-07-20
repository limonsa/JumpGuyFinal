using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySquareScript : MonoBehaviour
{
    public float speed = 2;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Hola Mundo");
        SpriteRenderer x = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    
    void Update()
    {
        // Moves an object forward, relative to its own rotation.
        //Debug.Log("UPDATE:   " + transform.position.x);
        
        if( transform.position.x < 5){
            speed++;
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
    }
}
