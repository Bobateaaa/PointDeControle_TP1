using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

/*
Code qui gère le déplacement du personnage, ses attaques,ses collisions, ses dégats, sa téléportation, la fin de la partie et la victoire
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

    public TextMeshProUGUI textFinPartie; // text fin de la partie
    public TextMeshProUGUI textCompteurAme;

    //VARIABLES ATTAQUE ET VIE DU PERSONNAGE

    public int ViePersonnage;  // compteur de vie du personnage
    public float vieActuelle; // vie actuelle du personnage
    private Image barreDeVie; // barre de vie du personnage
    
    public GameObject barreVie; // barre de vie du personnage
    int compteurAme; // compteur d'ame

    //Fonction qui initalise 
    void Start()
    {

        // Initialisation des variables
        peutAttaquer = true;
        finPartie = false;
        peutEtreAttaquer = true;
        ViePersonnage = 100;
        compteurAme = 0;

        // Initialisation de la source audio
        sourceAudio = GetComponent<AudioSource>();
        sourceAudio.clip = musiqueAmbianteJeu;
        sourceAudio.loop = true; 
        sourceAudio.Play();

        // On affiche les valeurs de vie et d'attaque du personnage
        textFinPartie.fontSize = 0; 
        vieActuelle = ViePersonnage;
        barreDeVie = barreVie.GetComponent<Image>();
        textCompteurAme.text = compteurAme.ToString();

}


    // Fonction  qui gère le deplacement du personnage, ses attaques et la fin de la partie
    void Update()
    {
        // Si la partie n'est pas terminée
        if(finPartie == false)
        {
        // On récupère les valeurs de vie et d'attaque du slime
            barreDeVie.fillAmount = ViePersonnage / vieActuelle;

            textCompteurAme.text = compteurAme.ToString();

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
                // On active l'animation de chute
                GetComponent<Animator>().SetBool("tomber", true);
            }
            // Sinon, on désactive l'animation de chute
            else
            {
                GetComponent<Animator>().SetBool("tomber", false);
            }

            // Si la touche "1" est enfoncée et que le personnage peut attaquer
            if (Input.GetKeyDown("1") && peutAttaquer == true)
            {
                // On active l'animation d'attaque 1
                GetComponent<Animator>().SetBool("attaque1", true);
                // Le personnage ne peut plus attaquer
                peutAttaquer = false;
                // On joue le son d'attaque 1
                sourceAudio.PlayOneShot(sonAttaque1, 2f);
                // On lance l'attaque 1
                lancerAttaque1();
                // On relance l'attaque
                Invoke("RelanceAttaque", 0.8f);
            }
            // Sinon, on désactive l'animation d'attaque 1
            else
            {
                GetComponent<Animator>().SetBool("attaque1", false);
            }

            // Si la touche "2" est enfoncée et que le personnage peut attaquer
            if (Input.GetKeyDown("2") && peutAttaquer == true)
            {
                // On active l'animation d'attaque 2
                GetComponent<Animator>().SetBool("attaque2", true);
                // Le personnage ne peut plus attaquer
                peutAttaquer = false;
                // On joue le son d'attaque 2
                sourceAudio.PlayOneShot(sonAttaque2, 2f);
                // On lance l'attaque 2
                lancerAttaque2();
                // On relance l'attaque
                Invoke("RelanceAttaque", 0.8f);
            }
            //sinon, on désactive l'animation d'attaque 2
            else
            {
                GetComponent<Animator>().SetBool("attaque2", false);
            }

            // Si la touche "3" est enfoncée et que le personnage peut attaquer
            if (Input.GetKeyDown("3") && peutAttaquer == true)
            {
                // On active l'animation d'attaque 3
                GetComponent<Animator>().SetBool("attaque3", true);
                // Le personnage ne peut plus attaquer
                peutAttaquer = false;
                // On joue le son d'attaque 3
                sourceAudio.PlayOneShot(sonAttaque3, 2f);
                // On lance l'attaque 3
                lancerAttaque3();
                // On relance l'attaque
                Invoke("RelanceAttaque", 0.9f);
            }
            // Sinon, on désactive l'animation d'attaque 3
            else
            {
                GetComponent<Animator>().SetBool("attaque3", false);
            }

            // Si la vie du personnage est inférieure ou égale à 0, la partie est terminée
            if (ViePersonnage <= 0)
            {
                // On arrête la musique
                finPartie = true;
                // On affiche le texte de fin de partie
                textFinEnPartie();
                // On relance le jeu
                Invoke("RelancerJeu", 5f);
            }
            
            // Si le personnage a ramassé 5 âmes, il a gagné la partie
            if(compteurAme == 5)
            {
                // On arrête la musique
                finPartie = true;
                // On affiche le texte de fin de partie
                textFinEnPartieVictoire();
                // On relance le jeu
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

        // Si le personnage entre en collision avec un slime, la berre de vie diminue
        if (collisionTrue.gameObject.name == "Slime" && peutEtreAttaquer == true) 
        {
            // Si le personnage entre en collision avec un ennemi, il perd de la vie
            ViePersonnage -= 5;
            // Le personnage ne peut plus être attaqué
            peutEtreAttaquer = false;
            // On relance la fonction peutEtreAttaquer
            Invoke("RelancerPeutEtreAttaquer", 1f);
            // On active l'animation de dégats
            GetComponent<Animator>().SetBool("prendDmg", true);
        }

        // Si le personnage entre en collision avec une pointe, la barre de vie diminue
        if (collisionTrue.gameObject.name == "Pointe" && peutEtreAttaquer == true) 
        {
            // Si le personnage entre en collision avec un ennemi, il perd de la vie
            ViePersonnage -= 7;
            // Le personnage ne peut plus être attaqué
            peutEtreAttaquer = false;
            // On relance la fonction peutEtreAttaquer
            Invoke("RelancerPeutEtreAttaquer", 1f);
            // On active l'animation de dégats
            GetComponent<Animator>().SetBool("prendDmg", true);
        }

        // Si le personnage entre en collision avec une ame, le compteur d'ame augmente
        if (collisionTrue.gameObject.tag == "Ame")
        {
            // Le compteur d'ame augmente
            compteurAme += 1;
            // On détruit l'ame
            Destroy(collisionTrue.gameObject);
        }
    }
   

    // Fonction qui gère les collisions du personnage avec les objets de téléportation
    void OnTriggerEnter2D(Collider2D TriggerTrue)
    {
        
        // Si le personnage entre en collision avec un objet de téléportation, il est téléporté au fort
        if (TriggerTrue.gameObject.name == "ObjetTeleportation" && estTeleporter == false)
        {
            // Le personnage est téléporté
            estTeleporter = true;
            // On relance la fonction de téléportation
            Invoke("RelancerTeleportation", 3f);
            // On déplace le personnage à la position de l'objet de téléportation
            gameObject.transform.position = objetTeleportationDonjon.transform.position;
        }
        // Si le personnage entre en collision avec un objet de téléportation, il est téléporté a la base
        else if (TriggerTrue.gameObject.name == "ObjetTeleportationDonjon" && estTeleporter == false)
        {
            // Le personnage est téléporté
            estTeleporter = true;
            // On relance la fonction de téléportation
            Invoke("RelancerTeleportation", 3f);
            // On déplace le personnage à la position de l'objet de téléportation
            gameObject.transform.position = objetTeleportationBase.transform.position;
        }
        
        // Si le personnage entre en collision avec l'attaque d'un feu des squelettes, la barre de vie diminue
        if (TriggerTrue.gameObject.tag == "Feu")
        {
            // Si le personnage entre en collision avec un ennemi, il perd de la vie
            ViePersonnage -= 10;
            // Le personnage ne peut plus être attaqué
            peutEtreAttaquer = false;
            // On relance la fonction peutEtreAttaquer
            Invoke("RelancerPeutEtreAttaquer", 1f);
            // On active l'animation de dégats
            GetComponent<Animator>().SetBool("prendDmg", true);
        }

    }

    // Fonction qui gère la relance le menu et le jeu
    void RelancerJeu()
    {
        // On relance l'intro
        SceneManager.LoadScene("SceneIntro");
    }

    // Fonction qui relance la possibilité d'attaquer
    void RelancerPeutEtreAttaquer()
    {
        // Le personnage peut être attaqué
        peutEtreAttaquer = true;
        // On désactive l'animation de dégats
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
        // Le personnage peut être téléporté
        estTeleporter = false;
    }

    // Fonction qui affiche le texte de fin de partie
    void textFinEnPartie()
    {
        textFinPartie.text = "Fin de la partie...";
        textFinPartie.fontSize = 40; 
    }
    void textFinEnPartieVictoire()
    {
        textFinPartie.text = "Victoire!";
        textFinPartie.fontSize = 40; 
    }
    
    // Fonction qui lance l'attaque 1
    void lancerAttaque1()
    {
        GameObject objetClone; // objet clone
        objetClone = Instantiate(attaqueOriginale); // on instancie l'attaque originale
        objetClone.SetActive(true); // on active l'attaque

        // Si le personnage n'est pas retourné, l'attaque est lancée vers la droite
        if(GetComponent<SpriteRenderer>().flipX == false)
                {
                    // on ne retourne pas l'attaque
                    objetClone.GetComponent<SpriteRenderer>().flipX = false; 
                    // on positionne l'attaque
                    objetClone.transform.position = transform.position + new Vector3(0.3f, 0.2f, 0);
                    // on donne une vélocité à l'attaque
                    objetClone.GetComponent<Rigidbody2D>().velocity= new Vector2(4f, 0);   
                }
                // Sinon, l'attaque est lancée vers la gauche
                else
                {
                    // on retourne l'attaque
                    objetClone.GetComponent<SpriteRenderer>().flipX = true;
                    // on positionne l'attaque
                    objetClone.transform.position = transform.position + new Vector3(-0.35f, 0.2f, 0);
                    // on donne une vélocité à l'attaque
                    objetClone.GetComponent<Rigidbody2D>().velocity = new Vector2(-4f, 0);  
                    // on gèle la position Y de l'attaque
                    objetClone.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
                }
        }

        // Fonction qui lance l'attaque 2 (memes commentaires que pour l'attaque 1 mais avec une vélocité qui permet une attaque en diagonale vers le haut)
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

        // Fonction qui lance l'attaque 3 (memes commentaires que pour l'attaque 1 mais avec une vélocité qui permet une attaque en diagonale vers le bas)  
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



