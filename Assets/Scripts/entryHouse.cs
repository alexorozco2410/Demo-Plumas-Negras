using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entryHouse : MonoBehaviour
{
    public GameObject floor2;
    public GameObject techo;   
    public GameObject exit;

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag.Equals("Player")) {
            techo.SetActive(false);
            floor2.SetActive(false);
            //    exit.SetActive(true);
            // this.gameObject.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag.Equals("Player")) {
            floor2.SetActive(true);
            techo.SetActive(true);
            //    exit.SetActive(true);
            // this.gameObject.SetActive(false);
        }
    }
}
