using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionAme : MonoBehaviour
{

    //fonction qui permet de détruire l'ame lorsqu'elle entre en collision avec un objet
    void OnCollisionEnter2D(Collision2D collisionTrue) 
    {
        // Si l'ame entre en collision avec le personnage on le détruit
        if (collisionTrue.gameObject.name == "Personnage")
        {

            Destroy(gameObject);
        }
    }

}
