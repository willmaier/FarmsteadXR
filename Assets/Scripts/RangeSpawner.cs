using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeSpawner : MonoBehaviour
{

    public GameObject enemyPrefab;
    public GameObject neuroPrefab;
    public float spawnTime = 5;

    public float xMin = -5;
    public float xMax = 5;
    public float zMin = -5;
    public float zMax = 5;

    // needs to be switched to false
    // player switches to true once they have gun and are ready
    public static bool isEnemyDead = true;
    public static bool isNeuroDead = true;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemies", spawnTime, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        //var enemyScript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<RangeEnemyBehavior>();
        //isEnemyDead = enemyScript.amIDead;

       // var neuroScript = GameObject.FindGameObjectWithTag("NT").GetComponent<ConfidenceLogger>();
        //isNeuroDead = neuroScript.isDead;
    }

    void SpawnEnemies()
    {
        if (isEnemyDead && isNeuroDead)
        {
            Vector3 enemyPosition;
            Vector3 neuroPosition;

            enemyPosition.x = Random.Range(xMin, xMax) + 177;
            enemyPosition.y = 0.3f;
            enemyPosition.z = Random.Range(zMin, zMax) + 78;

            neuroPosition.x = enemyPosition.x + Random.Range(xMin, xMax);
            neuroPosition.y = enemyPosition.y + Random.Range(0, 3);
            neuroPosition.z = enemyPosition.z + Random.Range(zMin, zMax);

            GameObject spawnedEnemy = Instantiate(enemyPrefab, enemyPosition, transform.rotation) as GameObject;
            GameObject spawnedNeuro = Instantiate(neuroPrefab, neuroPosition, transform.rotation) as GameObject;

            spawnedEnemy.transform.parent = gameObject.transform;
            spawnedNeuro.transform.parent = spawnedEnemy.transform;

            isEnemyDead = false;
            isNeuroDead = false;
        }
    }

    public static void updateDeaths()
    {
        isEnemyDead = true;
        isNeuroDead = true;
        Debug.Log("Got em both!");
    }
}
