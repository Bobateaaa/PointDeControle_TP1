using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleSlash : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
            Destroy(gameObject, 0.4f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ennemi")
        {
            GetComponent<Rigidbody2D>().angularVelocity = 0;
            Destroy(collision.gameObject); // Destroy the enemy
            Destroy(gameObject, 0.4f);

        }
    }



}
