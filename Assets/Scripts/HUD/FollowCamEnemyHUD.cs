using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamEnemyHUD : MonoBehaviour
{
    private Camera cam;

    void Start() {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    void Update() {
        transform.forward = cam.transform.forward;
    }
}
