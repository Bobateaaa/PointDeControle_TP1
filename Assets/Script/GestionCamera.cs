using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionCamera : MonoBehaviour
{   public GameObject cam1;
    public GameObject cam2;

    public GameObject cibleASuivre;

    public float limiteGauche;
    public float limiteDroite;
    public float limiteHaut;
    public float limiteBas;

    public float camDecalage;
    // Start is called before the first frame update
    void Start()
    {

        cam1.SetActive(true); //Active la caméra 1
        cam2.SetActive(false); //Désactive la caméra 2
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cibleASuivre.transform.position.x < 20)
        {
            cam1.SetActive(true); //Active la caméra 1
            cam2.SetActive(false); //Désactive la caméra 2
        }
        else 
        {
            cam1.SetActive(false); //Désactive la caméra 1
            cam2.SetActive(true); //Active la caméra 2

        Vector3 laPosition = cibleASuivre.transform.position;
        if (laPosition.x < limiteGauche) laPosition.x = limiteGauche;
        if (laPosition.x > limiteDroite) laPosition.x = limiteDroite;
        if (laPosition.y < limiteBas) laPosition.y = limiteBas;
        if (laPosition.y > limiteHaut) laPosition.y = limiteHaut;

        transform.position = new Vector3(laPosition.x, laPosition.y - camDecalage, transform.position.z);
        }
        
    }
}
