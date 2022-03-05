using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondFloorView : MonoBehaviour
{
    public GameObject techo;
    // Start is called before the first frame update
    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag.Equals("Player")) {
            techo.SetActive(false);
            // exit.SetActive(true);
            // this.gameObject.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag.Equals("Player")) {
            techo.SetActive(true);
            // exit.SetActive(true);
            // this.gameObject.SetActive(false);
        }
    }
}
