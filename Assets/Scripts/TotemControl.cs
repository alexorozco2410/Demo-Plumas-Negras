using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemControl : MonoBehaviour
{
    public AudioSource sourceTotem;
    public AudioClip clipRotTotem;
    public AudioClip clipAlertTotem;

    private bool isRotating = false;
    private float startRot = 0;
    private float speedRot = 45;
    private float currentRot = 0;
    public bool correctPosition = false;
    public bool isActiveTotem = false;

    public SpawnBossControl spawnBossScript;
    // Start is called before the first frame update
    void Start()
    {
        spawnBossScript = GameObject.FindGameObjectWithTag("spawnBoss").GetComponent<SpawnBossControl>();
    }

    // Update is called once per frame
    void Update()
    {
        RotatingTotem();
        activeSoundAlert();
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag.Equals("Player")) {
            RotTotem();
        }
    }

    void activeSoundAlert() {
        if (this.transform.rotation.eulerAngles.y >= -1 && this.transform.rotation.eulerAngles.y <= 1) {
            sourceTotem.PlayOneShot(clipAlertTotem, 0.3f);
            correctPosition = true;
            if (isActiveTotem == false) {
                spawnBossScript.checkActiveTotems();
                isActiveTotem = true;
            }
        } else {
            correctPosition = false;
        }
    }

    public void RotTotem() {
        if (Input.GetKeyDown("f")) {
            startRot = 0;
            currentRot = this.transform.rotation.eulerAngles.y;
            Debug.Log("current rot" + currentRot);
            isRotating = true;
            sourceTotem.PlayOneShot(clipRotTotem);
        }
    }

    public void RotatingTotem() {
        if (isRotating) {
            if (startRot < 45) {
                Vector3 aux = new Vector3(0, 1, 0) * speedRot * Time.fixedDeltaTime ;
                startRot += aux.magnitude;
                this.transform.Rotate(aux);
            } else {
                isRotating = false;
                fixedRotation();
            }
        }
    }

    public void fixedRotation() {
        /*Debug.Log("rot" + currentRot);
        Debug.Log("isrot" + isRotating);*/
        Debug.Log("rot" + this.transform.rotation.eulerAngles.y);
        switch (currentRot) {
            case 0:
                this.transform.rotation = Quaternion.Euler(0, 45, 0);
                break;
            case 45:
                this.transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            case 90:
                this.transform.rotation = Quaternion.Euler(0, 135, 0);
                break;
            case 135:
                this.transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
            case 180:
                this.transform.rotation = Quaternion.Euler(0, -135, 0);
                break;
            case -135:
                this.transform.rotation = Quaternion.Euler(0, -90, 0);
                break;
            case -90:
                this.transform.rotation = Quaternion.Euler(0, -45, 0);
                break;
            case -45:
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            default:
                break;
        }
    }
}
