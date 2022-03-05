using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootDamage : MonoBehaviour
{
    public int damage = 15;

    private void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "enemy1") {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }
}
