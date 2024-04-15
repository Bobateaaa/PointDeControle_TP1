using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
Script qui permet de jouer la musique d'intro et de naviguer entre des scenes différentes 
Auteur: Matilda Kang
Date: 2024-04-15
*/

public class BoutonIntro : MonoBehaviour
{
        //Déclaration des variables (publiques, privées et statiques)   
    public AudioClip musiqueAmbianteIntro; // son mort
    AudioSource sourceAudio; // source audio

    //Fonction qui s'exécute au démarrage
    void Start()
    {
        //Initialisation des variables
        sourceAudio = GetComponent<AudioSource>();
        sourceAudio.clip = musiqueAmbianteIntro;
        sourceAudio.loop = true; // Set the audio to loop
        sourceAudio.Play();
        
    }

    // Fonction qui s'exécute à chaque frame
    void Update()
    {
        //Si on appuie sur la touche "C", on charge la scène de jeu
        if(Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene("SceneDeJeu");
        }
        //Si on appuie sur la touche "O", on charge la scène qui comporte l'objectif
        if(Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadScene("SceneObjectif");
        }
        //Si on appuie sur la touche "R", on charge la scène qui comporte les règles du jeu
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SceneRegle");
        }
        
    }
}
