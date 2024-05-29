using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Code qui gère le comportement du slash, sa durée de vie et sa collision avec les ennemis
Auteur: Matilda Kang
Date: 2024-05-28
*/

public class ControleSlash : MonoBehaviour
{

    // Au démarrage, on détruit le slash après 0.4 secondes
    void Start()
    {
            Destroy(gameObject, 0.4f);
        
    }
    
    // Si le slash entre en collision avec un ennemi, on détruit l'ennemi
    void OnTriggerEnter2D(Collider2D collision)
    {

        // Si le slash entre en collision avec un ennemi, on détruit l'ennemi
        if(collision.gameObject.tag == "Ennemi")
        {
            // On détruit l'ennemi après 1 seconde
            Destroy(collision.gameObject, 1f);
            // On détruit le collider de l'ennemi
            Destroy(collision.gameObject.GetComponent<Collider2D>());
            // On arrete la rotation de l'ennemi
            GetComponent<Rigidbody2D>().angularVelocity = 0;
            // On détruit le slash
            Destroy(gameObject);

        }
    }



}
