using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squelette : MonoBehaviour
{
    public GameObject attaqueOriginale;
    public float vitesseAttaque;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetBool("attaque", true);
            InvokeRepeating("lancerAttaque", 0f, vitesseAttaque);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void lancerAttaque()
    {
        GameObject objetClone;
        objetClone = Instantiate(attaqueOriginale);
        objetClone.SetActive(true);

        if(GetComponent<SpriteRenderer>().flipX == false)
                {
                    objetClone.GetComponent<SpriteRenderer>().flipX = false;
                    objetClone.transform.position = transform.position + new Vector3(0.4f, 0f, 0);
                    objetClone.GetComponent<Rigidbody2D>().velocity= new Vector2(3f, 0);   
                }
                else
                {
                    objetClone.GetComponent<SpriteRenderer>().flipX = true;
                    objetClone.transform.position = transform.position + new Vector3(-0.45f, 0f, 0);
                    objetClone.GetComponent<Rigidbody2D>().velocity = new Vector2(-4f, 0);   
                }
    }
}

