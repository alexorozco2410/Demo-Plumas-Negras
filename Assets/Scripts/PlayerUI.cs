using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;
    public Camera cam;
    public GameObject arrow;
    public GameObject targetPoint;
    public GameObject parentUI;
    public GameObject targetUI;
    private bool canActiveAbility = true;
    public bool activeAbilityQ = false;

    public bool activeAbilityE = false;
    public bool activeAbilityR = false;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        /* if (canActiveAbility) {
            if (Input.GetKeyDown("q")) {
                arrow.SetActive(true);
                canActiveAbility = false;
                activeAbilityQ = true;
            }
        } */ 
        hideUI();
        RotateArrow();
        MoveTarget();
    }

    void hideUI(){
        if (Input.GetKeyUp("q")) {
            arrow.SetActive(false);
            canActiveAbility = true;
            activeAbilityQ = false;
        }
        if (Input.GetKeyUp("e")) {
            arrow.SetActive(false);
            canActiveAbility = true;
            activeAbilityE = false;
        }
        if (Input.GetKeyUp("r")) {
            targetPoint.SetActive(false);
            canActiveAbility = true;
            activeAbilityR = false;
        }
    }

    void RotateArrow() {
        if (activeAbilityE == true || activeAbilityQ == true){
            Ray rayClick = cam.ScreenPointToRay(Input.mousePosition);
            Vector3 mPos = cam.ScreenToWorldPoint(Input.mousePosition);
            if (Physics.Raycast(rayClick, out RaycastHit raycastHit, Mathf.Infinity, groundMask)){
                Vector3 pointClicked = raycastHit.point;
                Debug.Log("position click: " + pointClicked);
                var relative = (new Vector3(pointClicked.x, 0, pointClicked.z)) - (new Vector3(transform.position.x, 0, transform.position.z));
                var rot = Quaternion.LookRotation(relative,Vector3.up);
                // _input = Vector3.zero;
                // transform.rotation = rot; //makes the turn instantly
                // this.gameObject.GetComponent<RectTransform>().rotation = rot;
                parentUI.transform.rotation = rot;
            }
        }
    }

    void MoveTarget() {
        if (activeAbilityR == true){
            Ray rayClick = cam.ScreenPointToRay(Input.mousePosition);
            Vector3 mPos = cam.ScreenToWorldPoint(Input.mousePosition);
            if (Physics.Raycast(rayClick, out RaycastHit raycastHit, Mathf.Infinity, groundMask)){
                Vector3 pointClicked = raycastHit.point;
                
                targetUI.transform.position = new Vector3(pointClicked.x, targetUI.transform.position.y, pointClicked.z);
            }
        }
    }
}
