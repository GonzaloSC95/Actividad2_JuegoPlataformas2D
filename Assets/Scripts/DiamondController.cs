using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondController : MonoBehaviour
{
    //----Al declarar la variable puntos como estatica,
    //hacemos que pertenezca a todos los diamantes-----------
    private static int points = 0;
    public Text pointsText;
    //Guardamos el Script del player en una variable para que
    //podamos acceder a todos los metodos y variables publicas de dicho script;
    //ya que será el script del Player el que contenga el metodo encargado de reproducir
    //el sonido correspondiente al coger un diamante.
    private PlayerController playerScript;
    //--------------------
    private void Start()
    {
        playerScript = FindObjectOfType<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player") {
            points = points + 5;
            //Debug.Log("Puntos = " + points);
            pointsText.text = "Puntos: "+points.ToString();
            playerScript.diamondSoundPlayer();
            Destroy(gameObject);
        }
    }
}
