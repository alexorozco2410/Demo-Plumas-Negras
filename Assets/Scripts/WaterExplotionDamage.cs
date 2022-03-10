using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterExplotionDamage : MonoBehaviour
{
    void OnParticleCollision(GameObject other) {
        if(other.gameObject.tag == "enemy1") {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(3.5f);
        }
        if(other.gameObject.tag == "finalBoss") {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(3f);
        }
    }
}
