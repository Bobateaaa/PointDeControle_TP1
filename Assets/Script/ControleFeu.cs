using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleFeu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        void OnCollisionEnter2D(Collision2D collisionTrue) 
   {
        Destroy(gameObject, 0.1f);
    }
   

    void OnTriggerEnter2D(Collider2D collision)
    {
                Destroy(gameObject, 0.1f);

        if(collision.gameObject.name == "Personnage")
        {
            GetComponent<Rigidbody2D>().angularVelocity = 0;
            Destroy(gameObject, 0.4f);

        }
    }
}
