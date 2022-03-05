using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobeDamage : MonoBehaviour
{
    // public int damage = 30;

    public GameObject waterExplotionEffect;
    public GameObject WaterExplotion2;

    private void OnCollisionEnter(Collision other){
        if(other.gameObject.tag != "Player") {
            ContactPoint contact = other.contacts[0];
            Vector3 pos = contact.point;
            Quaternion rot = Quaternion.FromToRotation(waterExplotionEffect.transform.up, Vector3.up);
            Instantiate(WaterExplotion2, pos, rot);
            /*if(other.gameObject.tag == "enemy1") {
                other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            }*/
            Destroy(this.gameObject);
        }
    }
}
