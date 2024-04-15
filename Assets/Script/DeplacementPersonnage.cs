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
    public GameObject autreGameObject; // un autre game object
    EnnemiSlime scriptSlime; // le script slime
    int vieSlime; // compteur de vie du slime
    int attaqueSlime; // attaque du slime
    public int ViePersonnage;  // compteur de vie du personnage
    public int AttaquePersonnage; // attaque du personnage
    public TextMeshProUGUI textViePersonnage; // text vie personnage
    public TextMeshProUGUI textAttaquePersonnage; // text attaque personnage
    public TextMeshProUGUI textFinPartie; // text fin de la partie



    //Fonction qui s'exécute au démarrage du jeu
    void Start()
    {
        // On récupère le script de l'ennemi
        scriptSlime = autreGameObject.GetComponent<EnnemiSlime>();

        // Initialisation des variables
        peutAttaquer = true;
        finPartie = false;
        ViePersonnage = 10;
        AttaquePersonnage = 2;

        // Initialisation de la source audio
        sourceAudio = GetComponent<AudioSource>();
        sourceAudio.clip = musiqueAmbianteJeu;
        sourceAudio.loop = true; 
        sourceAudio.Play();

        // On récupère les valeurs de vie et d'attaque du slime
        vieSlime = scriptSlime.VieSlime;
        attaqueSlime = scriptSlime.AttaqueSlime;

        // On affiche les valeurs de vie et d'attaque du personnage
        textViePersonnage.text = "Vie: " + ViePersonnage;
        textAttaquePersonnage.text = "Attaque: " + AttaquePersonnage;
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
         textAttaquePersonnage.text = "Attaque: " + AttaquePersonnage;

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
                Invoke("RelanceAttaque", 0.9f);
            }
            else
            {
                GetComponent<Animator>().SetBool("attaque3", false);
            }

            // Si la touche les animations d'attaque sont vrai le personnage avance un peu ou recule un peu avec les attaques
            if (GetComponent<Animator>().GetBool("attaque1") == true 
            || GetComponent<Animator>().GetBool("attaque2") == true 
            || GetComponent<Animator>().GetBool("attaque3") == true 
            && peutAttaquer == false)
            {
                // Si le personnage peut attaquer, on ajuste la vélocité X
                if (GetComponent<Rigidbody2D>().velocity.x > 0 )
                {
                    vitesseX += 2f;
                }
                else if (GetComponent<Rigidbody2D>().velocity.x < 0 )
                {
                    vitesseX -= 2f;
                }
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

        // Si le personnage entre en collision avec un ennemi, il prend des dégats
        if(collisionTrue.gameObject.name == "Slime")
        {
            // Si le personnage ne peut pas attaquer
            if(peutAttaquer == false)
            {
                // quand le personnage attaque le slime prend du dommage
                scriptSlime.VieSlime -= AttaquePersonnage;
            }
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
    }

    // Fonction qui gère la relance le menu et le jeu
    void RelancerJeu()
    {
        SceneManager.LoadScene("SceneIntro");
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
    
}
