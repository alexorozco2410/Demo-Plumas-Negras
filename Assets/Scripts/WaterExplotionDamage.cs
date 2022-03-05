using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterExplotionDamage : MonoBehaviour
{
    void OnParticleCollision(GameObject other) {
        if(other.gameObject.tag == "enemy1") {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(3f);
        }
    }
}
