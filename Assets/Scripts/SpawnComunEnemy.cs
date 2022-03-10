using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnComunEnemy : MonoBehaviour
{
    public GameObject enemyC;
    public Transform[] spawnECPoints;
    private int countEnemyC = 0;
    private float countTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(EnemySpawn());   
    }

    // Update is called once per frame
    void Update()
    {
        if (countEnemyC < 8) {
           SpawnCommunEnemies();
        } else {
           //  StopAllCoroutines();
           countTime = 0;
        }
    }

    public void SpawnCommunEnemies() {
        countTime += 1 * Time.deltaTime;
            if (countTime > 3.0f){
                Instantiate(enemyC, spawnECPoints[Random.Range(0, spawnECPoints.Length)].position, Quaternion.identity);
                countEnemyC += 1;
                countTime = 0;
            }
    }

    /*IEnumerator EnemySpawn() {
        while (countEnemyC < 8){
            Instantiate(enemyC, spawnECPoints[Random.Range(0, spawnECPoints.Length)].position, Quaternion.identity);
            countEnemyC += 1;
            yield return new WaitForSeconds(2.0f);
        }
    }*/

    public void CommunEnemyDie() {
        countEnemyC -= 1;
    }
}
