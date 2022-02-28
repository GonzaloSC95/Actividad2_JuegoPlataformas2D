using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreatorController : MonoBehaviour
{
    //-------------------------
    public GameObject enemyPrefab;
    private Vector3 randomPosition;
    private GameObject enemy;
    public ArrayList enemies;
    //--------------------------
    void Start()
    {
        //Arrancamos la corruntina
        StartCoroutine("enemyCreator");
        //Inicializamos el arrayList
        enemies = new ArrayList();
    }

    IEnumerator enemyCreator()
    {
        //Cada x segundos, el juego hace una pausa.
        yield return new WaitForSeconds(5f);
        //Transcurrida la pausa, genera un nuevo enemigo.
        while (true)
        {
            randomPosition = new Vector3(Random.Range(6f, 70f), 0, 0);
            //(GameObject,Positin,Rotation)
            enemy=Instantiate(enemyPrefab, randomPosition, Quaternion.identity);//Quaternion.identity=Sin rotación
            enemies.Add(enemy);
            ////Cada x segundos, el juego hace una pausa.
            yield return new WaitForSeconds(Random.Range(4f, 5f)); //Float(min y max incluidos) Integer(min incluido,max excluido)
        }
    }

    public void destroyAllEnemies() {
        Destroy(gameObject);
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy,1.5f);
        }
    }

}