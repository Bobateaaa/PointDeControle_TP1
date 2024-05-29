using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Code qui gère les boules de feu et leur collision avec les objets du jeu et le personnage
Auteur: Matilda Kang
Date: 2024-05-28
*/

public class ControleFeu : MonoBehaviour
{
    // fonction qui permet de détruire le feu lorsqu'il entre en collision avec un objet
    void OnCollisionEnter2D(Collision2D collisionTrue) 
    {
        // Si le feu entre en collision avec un objet on le détruit
        Destroy(gameObject, 0.1f);
    }
   
    // fonction qui permet de détruire le feu lorsqu'il entre en collision avec un objet
    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject, 0.1f);

        // Si le feu entre en collision avec le personnage, on le détruit
        if(collision.gameObject.name == "Personnage")
        {
            GetComponent<Rigidbody2D>().angularVelocity = 0;
            Destroy(gameObject, 0.4f);

        }
    }
}
