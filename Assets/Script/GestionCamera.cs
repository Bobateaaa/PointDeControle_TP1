using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Code qui gère les caméras et leur déplacement
Auteur: Matilda Kang
Date: 2024-05-28
*/

public class GestionCamera : MonoBehaviour
{   public GameObject cam1; //la caméra 1
    public GameObject cam2; //la caméra 2
    public GameObject cibleASuivre; // la cible à suivre

    public float limiteGauche; //les limites de la caméra
    public float limiteDroite; //les limites de la caméra
    public float limiteHaut; //les limites de la caméra
    public float limiteBas; //les limites de la caméra

    public float camDecalage; //le décalage de la caméra

    // initialisation des caméras
    void Start()
    {
        cam1.SetActive(true); //Active la caméra 1
        cam2.SetActive(false); //Désactive la caméra 2
    }

    // Fonction qui permet de suivre la cible à l'écran
    void Update()
    {
        // Si la cible à suivre est à gauche de la position 20, on active la caméra 1
        if(cibleASuivre.transform.position.x < 20)
        {
            cam1.SetActive(true); //Active la caméra 1
            cam2.SetActive(false); //Désactive la caméra 2
        }
        // Sinon, on active la caméra 2
        else 
        {
            cam1.SetActive(false); //Désactive la caméra 1
            cam2.SetActive(true); //Active la caméra 2

        // On limite la caméra à un certain endroit
        Vector3 laPosition = cibleASuivre.transform.position;
        if (laPosition.x < limiteGauche) laPosition.x = limiteGauche;
        if (laPosition.x > limiteDroite) laPosition.x = limiteDroite;
        if (laPosition.y < limiteBas) laPosition.y = limiteBas;
        if (laPosition.y > limiteHaut) laPosition.y = limiteHaut;

        // On déplace la caméra
        transform.position = new Vector3(laPosition.x, laPosition.y - camDecalage, transform.position.z);
        }
        
    }
}
