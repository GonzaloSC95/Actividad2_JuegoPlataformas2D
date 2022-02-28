using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyController : MonoBehaviour
{
    //-------------------
    private PlayerController playerScript;
    //------------------
    private void Start()
    {
        playerScript = FindObjectOfType<PlayerController>();
    }
    private void Update()
    {
        transform.Rotate(Vector3.up);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si el Player encuentra la llave........
        if (collision.gameObject.tag == "Player")
        {
            //Buscamos en la jerarquia el gameObject que tenga el nombre de "chestController".
            //De esta forma obtenemos el script y podemos acceder
            //a todos su metodos y sus variables, siempre que sean publicas
            FindObjectOfType<chestController>().has_key = true;
            playerScript.keySoundPlayer();
            //Acto seguido la llave desaparece.
            Destroy(gameObject);
        }
    }
}
