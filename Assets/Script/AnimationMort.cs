using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Code qui gère l'animation de mort du slime
Auteur: Matilda Kang
Date: 2024-05-28
*/

public class Slime : MonoBehaviour
{

    // Fonction qui permet de déclencher l'animation de mort du slime
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Si le slime est touché par l'attaque du joueur
        if(collision.gameObject.tag == "Slash")
        {
            GetComponent<Animator>().SetTrigger("mort");

        }
    }
}
