using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Script qui permet de gérer les ennemis de type slime
Auteur: Matilda Kang
Date: 2024-04-15
*/

public class EnnemiSlime : MonoBehaviour
{

    //Déclaration des variables (publiques, privées et statiques)
    public int VieSlime; // compteure de vie du slime
    public int AttaqueSlime; // nombre attaque du slime
    bool attaque; // si le slime attaque
    public GameObject autreGameObject; // GameObject de l'autre personnage
    DeplacementPersonnage scriptPersonnage; // script du personnage


    //Fonction qui s'exécute au démarrage
    void Start()
    {
        //Initialisation des variables
        attaque = false;
        scriptPersonnage = autreGameObject.GetComponent<DeplacementPersonnage>();
        VieSlime = 10;
        AttaqueSlime = 1;    
    }

    // Fonction qui s'exécute à chaque frame
    void Update()
    {
        //Si la vie du slime est inférieure ou égale à 0, le slime meurt
        if(VieSlime <= 0)
        {
            GetComponent<Animator>().SetBool("mort", true);
            Invoke("DestructionEnnemi", 1.5f);
        }
        Debug.Log("VieSlime: " + VieSlime);
    }

    // Fonction qui s'exécute lorsqu'un objet entre en collision avec le slime
    void OnCollisionEnter2D(Collision2D collisionTrue) 
    {
        //Si le slime entre en collision avec le personnage et qu'il n'a pas encore attaqué, le slime attaque le personnage
        if(collisionTrue.gameObject.name == "AnimPersonnage" && attaque == false)
        {
            attaque = true;
            scriptPersonnage.ViePersonnage -= AttaqueSlime;
            Invoke("RelancerAttaque", 0.5f);
        }
    }

    // Fonction qui permet de relancer une attaque
    void RelancerAttaque()
    {
        attaque = false;
    }

    // Fonction qui permet de détruire le slime lorsqu'il meurt
    void DestructionEnnemi()
    {
        Destroy(gameObject);
    }
}