using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBossControl : MonoBehaviour
{
    public GameObject[] totems;
    public int[] activeTotems = new int[] {0,0,0,0};
    public int spawn = 0;
    public GameObject bossPrefab;
    public bool instanceBoss = false;

    public AudioSource sourceSpawnBoss;
    public AudioClip clipSpawnBoss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void checkActiveTotems() {
       if (instanceBoss == false) {
            Debug.Log("checando totems");
            int index = 0;
            spawn = 0;
            foreach (GameObject totem in totems) {
                if (totem.GetComponent<TotemControl>().correctPosition == false) {
                activeTotems[index] = 0;
                } else {
                    activeTotems[index] = 1;
                }
                spawn += activeTotems[index];
                index += 1;
            }
            if (spawn >= 4) {
                // sourceSpawnBoss.PlayOneShot(clipSpawnBoss);
                sourceSpawnBoss.PlayDelayed(2);
                Instantiate(bossPrefab, transform.position, Quaternion.identity);
                instanceBoss = true;
            }
       }
    }
}
