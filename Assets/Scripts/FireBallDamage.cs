using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallDamage : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter(Collider other){
        if(other.tag == "Player") {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
        if(other.tag != "enemy1") {
            Destroy(this.gameObject);
        }
    }
}
