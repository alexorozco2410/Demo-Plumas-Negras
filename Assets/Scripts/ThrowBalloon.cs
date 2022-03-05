using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBalloon : MonoBehaviour
{
    public GameObject ballGO;
    public Transform objTrans;
    public float h=5;
    public float gravity = -9.8f;

    public bool isThrowing = false;

    public Camera cam;
    [SerializeField] private LayerMask groundMask;

    public Transform globeInitialPos;

    public GameObject globePrefab;

    private GameObject tmpGlobe;

    public Vector3 pointObjective = new Vector3 (0,0,0);

    // Update is called once per frame
    void Update()
    {
        // updateIsThrowing();
        if (isThrowing) {
            isThrowing = false;
            tmpGlobe = Instantiate(globePrefab, globeInitialPos.position, Quaternion.identity);
            Lanzar();
        }
    }

    public void setPositionObjective(Vector3 objective) {
        pointObjective = objective;
    }
    public void updateIsThrowing() {
        // pointObjective = objective;
        isThrowing = !isThrowing;
    }

    void Lanzar() {
        Rigidbody ballRB = tmpGlobe.GetComponent<Rigidbody>();
        Physics.gravity = Vector3.up * gravity;
        ballRB.useGravity = true;
        ballRB.velocity = CalculateVI();
        print(ballRB.velocity);
    }

    Vector3 CalculateVI() {
        Vector3 pointClicked = pointObjective;
        /*Vector3 pointClicked = new Vector3 (0, 0, 10);

        Ray rayClick = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 mPos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Physics.Raycast(rayClick, out RaycastHit raycastHit, Mathf.Infinity, groundMask)){
            pointClicked = raycastHit.point;
        }*/
        /*float moveY = pointClicked.y - ballGO.transform.position.y;
        float moveX = pointClicked.x - ballGO.transform.position.x;
        float moveZ = pointClicked.z - ballGO.transform.position.z;*/

        Vector3 moveP = pointClicked - tmpGlobe.transform.position;

        float velocidadX, velocidadY, velocidadZ;
        velocidadY = Mathf.Sqrt(-2 * gravity * h);
        velocidadX = moveP.x / ( (-velocidadY / gravity) + Mathf.Sqrt(2 * (moveP.y -h) / gravity) );
        velocidadZ = moveP.z / ( (-velocidadY / gravity) + Mathf.Sqrt(2 * (moveP.y -h) / gravity) );

        return new Vector3(velocidadX, velocidadY, velocidadZ);
    }
}
