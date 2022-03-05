using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryShop : MonoBehaviour
{
    public GameObject techo;   
    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag.Equals("Player")) {
            techo.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag.Equals("Player")) {
            techo.SetActive(true);
        }
    }
}
