using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitHouse : MonoBehaviour
{
    public GameObject floor2;
    public GameObject techo; 

    public GameObject entry;
    
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("Player")) {
            floor2.SetActive(true);
            techo.SetActive(true);
            entry.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
