using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //--------------
    private float speed=5f;
    private float jumpForce = 6F;
    private float movement_X;
    private float movement_Y;
    private Rigidbody2D rigidBody;
    private SpriteRenderer flipPlayer;
    private bool isGround=true;
    private AudioSource audioManager;
    public AudioClip diamondSoundCollect, jumpSound,attack,keyCollect;
    public GameObject cameraPlayer;
    public GameObject explosionMuerteDelPlayer;
    private Animator animatorAttack;
    public GameObject panelGameOver;
    private EnemyCreatorController enemyCreatorController_Script;
    //--------------
    //Metodo que se ejecuta al iniciar la aplicación
    void Start()
    {
        //--------------------------
        flipPlayer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        audioManager = GetComponent<AudioSource>();
        animatorAttack = GetComponent<Animator>();
        enemyCreatorController_Script = FindObjectOfType<EnemyCreatorController>();
        //----------------------------------
    }

    // Metodo que se ejecuta por cada frame del juego
    void Update()
    {
        //-------------------------
        movimiento_horizontal();
        salto();
        attackMode();
        //-------------------------
    }

    void movimiento_horizontal() {
        movement_X = Input.GetAxis("Horizontal");
        //-----CONTROLAMOS SI EL MOVIMIENTO EN EL EJE X ES > O < QUE 0 PARA DARLE LA VUELTA AL PERSONAJE-----------
        if (movement_X > 0)
        {
            flipPlayer.flipX = false;
        }
        if (movement_X < 0)
        {
            flipPlayer.flipX = true;
        }
        //--------------------------------------------------
        //Obtenemos la componente transform del personaje para poder moverlo or el nivel
        //Al no poner el objeto, los componentes a los que se accede son
        //los del GameObject asignado a este script (this.transform.position)
        transform.position += new Vector3(movement_X, 0, 0) * speed * Time.deltaTime;
        //Multiplicar por Time.deltaTime nos asegurará que el movimiento será fluido
        //independientemenete de la velocidad del microprocesador
        //Movimiento horizontal (x,y=0,z=0)
        //En Edit/Proyect Settings/Input Manager podemos ver la configuración de controles de Unity y nuestro proyecto

    }

    void salto() {
        //movement_Y = Input.GetAxis("Vertical");
        //transform.position = transform.position+ new Vector3(0,movement_Y,0)*speed*Time.deltaTime;
        //Para hacer el controlador de salto, vamos a hacerlo a través del componente RigidBody
        //Si la tecla "Flecha hacia arriba" es pulsada y el personaje esta en el suelo....
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGround==true) {
            //Añadimos una fuerza de impulso en dirección Y (Vector2.up) , en modo impulso (ForceMode2D.Impulse)
            rigidBody.AddForce(Vector2.up* jumpForce,ForceMode2D.Impulse);
            //Reproducimos el sonido correspondiente al saltar
            jumpSoundPlayer();
        }
    }


    //Metodos para controlar las colisiones del Player con el resto de objetos del juego--------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Si el Player colisiona con el suelo.....
        if (collision.gameObject.tag=="Ground") {
            isGround = true;
        }

        //Si el Player entra en contacto con un enemigo
        if (collision.gameObject.tag=="Enemy") {
            Debug.Log("Muerte del Player - Fin del juego");
            //---------Antes de destruir al Player hay que separarlo de la Main camera---------------
            //Siempre tiene que haber minimo 1 camara e el videojuego
            cameraPlayer.transform.parent = null;
            //-----Ahora si destruimos/matamos al Player e instanciamos el sistema de particulas correspondiente---------------
            Instantiate(explosionMuerteDelPlayer, transform.position+new Vector3(0,0.3f,0), Quaternion.identity);
            //----Activamos el panel de Fin del Juego-------
            panelGameOver.SetActive(true);
            //-Destuimos al Player------------
            Destroy(gameObject);
            enemyCreatorController_Script.destroyAllEnemies();
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Si el Player deja de colisionar con el suelo.....
        if (collision.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }

    public void diamondSoundPlayer() {
        audioManager.PlayOneShot(diamondSoundCollect);
    }

    public void keySoundPlayer()
    {
        audioManager.PlayOneShot(keyCollect);
    }

    private void jumpSoundPlayer() {
        audioManager.PlayOneShot(jumpSound);
    }

    void attackMode() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            //Activamos la animacion de ataque
            animatorAttack.SetTrigger("playerAttack");
            //Activamos el sonido de ataque
            audioManager.PlayOneShot(attack);
        }
    }
}
