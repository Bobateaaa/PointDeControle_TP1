using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnerAccueil : MonoBehaviour
{
    //Fonction qui permet de retourner à l'accueil
    void Update()
    {
        if(Input.GetKey("backspace"))
        {
            SceneManager.LoadScene("SceneIntro");
        }
        
    }
}
