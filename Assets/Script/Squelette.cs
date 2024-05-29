using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Code qui gère le clonage de l'attaque du squelette (boules de feu) et sa direction
Auteur: Matilda Kang
Date: 2024-05-28
*/

public class Squelette : MonoBehaviour
{
    public GameObject attaqueOriginale; //attaque du squelette
    public float vitesseAttaque; //vitesse d'attaque du squelette

    // Fonction qui permet de lancer l'attaque du squelette en boucle
    void Start()
    {
        //lancer l'animation d'attaque
        GetComponent<Animator>().SetBool("attaque", true);
        //lancer l'attaque en boucle
        InvokeRepeating("lancerAttaque", 0f, vitesseAttaque);

    }

    // Fonction qui permet de lancer l'attaque du squelette
    void lancerAttaque()
    {
        GameObject objetClone; //objet qui va être cloné
        objetClone = Instantiate(attaqueOriginale); //cloner l'attaque du squelette
        objetClone.SetActive(true); //activer l'attaque du squelette

        //si le squelette est tourné vers la droite
        if(GetComponent<SpriteRenderer>().flipX == false)
        {
            //faire en sorte que l'attaque soit tournée vers la droite
            objetClone.GetComponent<SpriteRenderer>().flipX = false;
            //positionner l'attaque du squelette
            objetClone.transform.position = transform.position + new Vector3(0.4f, 0f, 0);
            //donner une vitesse à l'attaque
            objetClone.GetComponent<Rigidbody2D>().velocity= new Vector2(3f, 0);   
        }
        //si le squelette est tourné vers la gauche
        else
        {
            //faire en sorte que l'attaque soit tournée vers la gauche
            objetClone.GetComponent<SpriteRenderer>().flipX = true;
            //positionner l'attaque du squelette
            objetClone.transform.position = transform.position + new Vector3(-0.45f, 0f, 0);
            //donner une vitesse à l'attaque
            objetClone.GetComponent<Rigidbody2D>().velocity = new Vector2(-4f, 0);   
        }
    }
}

