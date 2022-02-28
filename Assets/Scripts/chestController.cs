using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestController : MonoBehaviour
{
    //-------------------
    private Animator animatorChest;
    public bool has_key=false;
    public GameObject panelWin;
    public GameObject player;
    public GameObject cameraMain;
    private EnemyCreatorController enemyCreatorController_Script;
    public GameObject enemiesGenerator;
    //---------------------

    private void Start()
    {
        animatorChest = GetComponent<Animator>();
        enemyCreatorController_Script = FindObjectOfType<EnemyCreatorController>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Player") {
            if (has_key == true) { 
                animatorChest.SetBool("isOpen", true);
                panelWin.SetActive(true);
                //----------------------------------
                enemyCreatorController_Script.destroyAllEnemies();
            }
            
        }
    }
}
