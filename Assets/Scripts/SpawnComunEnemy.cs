using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnComunEnemy : MonoBehaviour
{
    public GameObject enemyC;
    public Transform[] spawnECPoints;
    private int countEnemyC = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EnemySpawn() {
        while (countEnemyC < 10){
            Instantiate(enemyC, spawnECPoints[Random.Range(0, spawnECPoints.Length)].position, Quaternion.identity);
            countEnemyC += 1;
            yield return new WaitForSeconds(2.0f);
        }
    }
}
