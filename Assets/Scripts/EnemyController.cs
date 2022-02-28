using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //---------------------------------
    private Transform playerPosition;
    private float speed = 2f;
    private SpriteRenderer flipEnemy;
    //----------------------------------
    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.Find("Player").transform;
        flipEnemy = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Hacemos que el enemigo se desplace constantemente desde su posición hacia la posción en la que se encuentra el Player
        transform.position = Vector3.MoveTowards(transform.position,playerPosition.position,speed * Time.deltaTime);
        //Si la posición del enemigo en el eje x, es menor que la del player, cambiamos el flip x a true;
        if (transform.position.x < playerPosition.position.x) {
            flipEnemy.flipX = true;
        } else {
            flipEnemy.flipX = false;
        }
    }
}
