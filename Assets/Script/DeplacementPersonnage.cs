using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/*
Code qui gère le déplacement du personnage, ses attaques,ses collisions, ses dégats et sa téléportation
Auteur: Matilda Kang
Date: 2024-04-15
*/

public class DeplacementPersonnage : MonoBehaviour
{

    //Déclaration des variables (publiques, privées et statiques)    
    public float vitesseX; //vitesse de déplacement horizontale
    public float vitesseY; //  vitesse de déplacement vertical
    bool peutAttaquer; // peut attaquer
    bool peutEtreAttaquer; // peut être attaquer
    bool finPartie; // fin de la partie
    public AudioClip musiqueAmbianteJeu; // son mort
    public AudioClip sonAttaque1; // son attaque 1
    public AudioClip sonAttaque2; // son attaque 2
    public AudioClip sonAttaque3; // son attaque 3
    public AudioClip sonSaut; // son saut
    AudioSource sourceAudio; // source audio
    bool estTeleporter; // est teleporter
    public GameObject objetTeleportationBase; // objet teleportation dans l'environnent de gazon
    public GameObject objetTeleportationDonjon; // objet teleportation dans l'environnent du fort/donjon
    public GameObject attaqueOriginale; // objet teleportation

    public TextMeshProUGUI textViePersonnage; // text vie personnage
    public TextMeshProUGUI textAttaquePersonnage; // text attaque personnage
    public TextMeshProUGUI textFinPartie; // text fin de la partie

    //VARIABLES ATTAQUE ET VIE DU PERSONNAGE

    public int ViePersonnage;  // compteur de vie du personnage

    //Fonction qui s'exécute au démarrage du jeu
    void Start()
    {

        // Initialisation des variables
        peutAttaquer = true;
        finPartie = false;
        peutEtreAttaquer = true;
        ViePersonnage = 10;

        // Initialisation de la source audio
        sourceAudio = GetComponent<AudioSource>();
        sourceAudio.clip = musiqueAmbianteJeu;
        sourceAudio.loop = true; 
        sourceAudio.Play();

        // On affiche les valeurs de vie et d'attaque du personnage
        textViePersonnage.text = "Vie: " + ViePersonnage;
        textFinPartie.fontSize = 0; 

}


    // Fonction qui s'exécute à chaque frame
    void Update()
    {
        // Si la partie n'est pas terminée
        if(finPartie == false)
        {
        // On récupère les valeurs de vie et d'attaque du slime
         textViePersonnage.text = "Vie: " + ViePersonnage;

            //On ajuste la variable vitesseX si la touche "a" ou "w" est appuyée
            if (Input.GetKey("a") || Input.GetKey("right"))
            {
                vitesseX = 2f;
                GetComponent<SpriteRenderer>().flipX = false;
                GetComponent<Animator>().SetBool("course", true);
            }
            else if (Input.GetKey("d") || Input.GetKey("left"))
            {
                vitesseX = -2f;
                GetComponent<SpriteRenderer>().flipX = true;
                GetComponent<Animator>().SetBool("course", true);
            }
            // Si aucune touche n'est enfoncée, on récupère la vélocité X actuelle
            else
            {
                vitesseX = GetComponent<Rigidbody2D>().velocity.x;
                GetComponent<Animator>().SetBool("course", false);
                GetComponent<Animator>().SetBool("saut", false);


            }

            //On ajuste la variable vitesseY si la touche w est enfoncée
            if (Input.GetKeyDown("w") || Input.GetKeyDown("up") && Physics2D.OverlapCircle(transform.position, 0.25f))
            {
                vitesseY = 5f ;
                sourceAudio.PlayOneShot(sonSaut, 1f);
                GetComponent<Animator>().SetBool("saut", true);
            }
            // Si aucune touche n'est enfoncée, on récupère la vélocité Y actuelle
            else
            {
                GetComponent<Animator>().SetBool("saut", false);
                vitesseY = GetComponent<Rigidbody2D>().velocity.y;
            }
            

            // si la vélocité Y est inférieure ou égale à 0, le personnage tombe
            if (GetComponent<Rigidbody2D>().velocity.y <= 0)
            {
                GetComponent<Animator>().SetBool("tomber", true);
            }
            else
            {
                GetComponent<Animator>().SetBool("tomber", false);
            }

            // Si la touche "1" est enfoncée et que le personnage peut attaquer
            if (Input.GetKeyDown("1") && peutAttaquer == true)
            {
                GetComponent<Animator>().SetBool("attaque1", true);
                peutAttaquer = false;
                sourceAudio.PlayOneShot(sonAttaque1, 2f);
                lancerAttaque1();
                Invoke("RelanceAttaque", 0.8f);
            }
            else
            {
                GetComponent<Animator>().SetBool("attaque1", false);
            }

            // Si la touche "2" est enfoncée et que le personnage peut attaquer
            if (Input.GetKeyDown("2") && peutAttaquer == true)
            {
                GetComponent<Animator>().SetBool("attaque2", true);
                peutAttaquer = false;
                sourceAudio.PlayOneShot(sonAttaque2, 2f);
                lancerAttaque2();
                Invoke("RelanceAttaque", 0.8f);
            }
            else
            {
                GetComponent<Animator>().SetBool("attaque2", false);
            }

            // Si la touche "3" est enfoncée et que le personnage peut attaquer
            if (Input.GetKeyDown("3") && peutAttaquer == true)
            {
                GetComponent<Animator>().SetBool("attaque3", true);
                peutAttaquer = false;
                sourceAudio.PlayOneShot(sonAttaque3, 2f);
                lancerAttaque3();
                Invoke("RelanceAttaque", 0.9f);
            }
            else
            {
                GetComponent<Animator>().SetBool("attaque3", false);
            }


            // Si la vie du personnage est inférieure ou égale à 0, la partie est terminée
            if (ViePersonnage <= 0)
            {
                finPartie = true;
                textFinEnPartie();
                Invoke("RelancerJeu", 5f);
            }

        // On ajuste la vélocité du personnage en lui attribuant la valeur de la variable locale
        GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX,vitesseY);
        }
        // Si la partie est terminée tous les touches se desactivent
        else
        {

        }
        
    }

        // Fonction qui gère les collisions du personnage avec les ennemis et son environement
    void OnCollisionEnter2D(Collision2D collisionTrue) 
   {
           // Si le personnage entre en collision avec un objet, il ne saute plus
        if (Physics2D.OverlapCircle(transform.position, 0.25f))
        {
            // Désactive l'animation de saut
            GetComponent<Animator>().SetBool("saut", false);
        } 

        if (collisionTrue.gameObject.name == "Slime" && peutEtreAttaquer == true) 
        {
            // Si le personnage entre en collision avec un ennemi, il perd de la vie
            ViePersonnage -= 1;
            peutEtreAttaquer = false;
            Invoke("RelancerPeutEtreAttaquer", 1f);
            GetComponent<Animator>().SetBool("prendDmg", true);
        }

        if (collisionTrue.gameObject.name == "Pointe" && peutEtreAttaquer == true) 
        {
            // Si le personnage entre en collision avec un ennemi, il perd de la vie
            ViePersonnage -= 1;
            peutEtreAttaquer = false;
            Invoke("RelancerPeutEtreAttaquer", 1f);
            GetComponent<Animator>().SetBool("prendDmg", true);
        }

        

        
    }
   

    // Fonction qui gère les collisions du personnage avec les objets de téléportation
    void OnTriggerEnter2D(Collider2D TriggerTrue)
    {
        // Si le personnage entre en collision avec un objet de téléportation, il est téléporté au fort
        if (TriggerTrue.gameObject.name == "ObjetTeleportation" && estTeleporter == false)
        {
            estTeleporter = true;
            Invoke("RelancerTeleportation", 3f);
            gameObject.transform.position = objetTeleportationDonjon.transform.position;
        }
        // Si le personnage entre en collision avec un objet de téléportation, il est téléporté a la base
        else if (TriggerTrue.gameObject.name == "ObjetTeleportationDonjon" && estTeleporter == false)
        {
            estTeleporter = true;
            Invoke("RelancerTeleportation", 3f);
            gameObject.transform.position = objetTeleportationBase.transform.position;
        }

        if (TriggerTrue.gameObject.tag == "Feu")
        {
            // Si le personnage entre en collision avec un ennemi, il perd de la vie
            ViePersonnage -= 1;
            peutEtreAttaquer = false;
            Invoke("RelancerPeutEtreAttaquer", 1f);
            GetComponent<Animator>().SetBool("prendDmg", true);
        }
    }

    // Fonction qui gère la relance le menu et le jeu
    void RelancerJeu()
    {
        SceneManager.LoadScene("SceneIntro");
    }

    void RelancerPeutEtreAttaquer()
    {
        peutEtreAttaquer = true;
        GetComponent<Animator>().SetBool("prendDmg", false);
    }
    
    // Fonction qui relance l'attaque
    void RelanceAttaque()
    {
        // Le personnage peut attaquer
        peutAttaquer = true;
    }

    // Fonction qui relance la téléportation
    void RelancerTeleportation()
    {
        estTeleporter = false;
    }

    // Fonction qui affiche le texte de fin de partie
    void textFinEnPartie()
    {
    textFinPartie.text = "Fin de la partie...";
    textFinPartie.fontSize = 40; // Change this to the size you want
    }
    
    void lancerAttaque1()
    {
        GameObject objetClone;
        objetClone = Instantiate(attaqueOriginale);
        objetClone.SetActive(true);

        if(GetComponent<SpriteRenderer>().flipX == false)
                {
                    objetClone.GetComponent<SpriteRenderer>().flipX = false;
                    objetClone.transform.position = transform.position + new Vector3(0.3f, 0.2f, 0);
                    objetClone.GetComponent<Rigidbody2D>().velocity= new Vector2(4f, 0);   
                }
                else
                {
                    objetClone.GetComponent<SpriteRenderer>().flipX = true;
                    objetClone.transform.position = transform.position + new Vector3(-0.35f, 0.2f, 0);
                    objetClone.GetComponent<Rigidbody2D>().velocity = new Vector2(-4f, 0);  
                    objetClone.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
                }
    }

            void lancerAttaque2()
        {
            GameObject objetClone;
            objetClone = Instantiate(attaqueOriginale);
            objetClone.SetActive(true);
        
            if(GetComponent<SpriteRenderer>().flipX == false)
            {
                objetClone.GetComponent<SpriteRenderer>().flipX = false;
                objetClone.transform.position = transform.position + new Vector3(0.3f, 0.2f, 0);
                objetClone.GetComponent<Rigidbody2D>().velocity= new Vector2(4f, 2f); // Adjusted velocity
            }
            else
            {
                objetClone.GetComponent<SpriteRenderer>().flipX = true;
                objetClone.transform.position = transform.position + new Vector3(-0.35f, 0.2f, 0);
                objetClone.GetComponent<Rigidbody2D>().velocity = new Vector2(-4f, 2f); // Adjusted velocity
            }
        }

        void lancerAttaque3()
        {
            GameObject objetClone;
            objetClone = Instantiate(attaqueOriginale);
            objetClone.SetActive(true);
        
            if(GetComponent<SpriteRenderer>().flipX == false)
            {
                objetClone.GetComponent<SpriteRenderer>().flipX = false;
                objetClone.transform.position = transform.position + new Vector3(0.3f, 0.2f, 0);
                objetClone.GetComponent<Rigidbody2D>().velocity= new Vector2(4f, -2f); // Adjusted velocity
            }
            else
            {
                objetClone.GetComponent<SpriteRenderer>().flipX = true;
                objetClone.transform.position = transform.position + new Vector3(-0.35f, 0.2f, 0);
                objetClone.GetComponent<Rigidbody2D>().velocity = new Vector2(-4f, -2f); // Adjusted velocity
            }
        }


}
