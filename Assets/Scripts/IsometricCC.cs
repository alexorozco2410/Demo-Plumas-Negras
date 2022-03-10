using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class IsometricCC : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    public float _speed = 10;
    private float magnitudeMove = 0;
    private Animator anim;
    private Vector3 _input;
    public Camera cam;
    [SerializeField] private LayerMask groundMask;
    public bool setColliderHeight = false;
    public CapsuleCollider playerCapsuleCollider;
    public bool isActiveGun = false;
    public bool gotGun = false;
    public GameObject Gun;

    private bool isActiveAbE = true;
    private float coolDownE = 6;
    private float startCoolDownE = -1;

    private bool isActiveAbQ = true;
    private float coolDownQ = 1;
    private float startCoolDownQ = -1;

    private float startCoolDownG = -1;
    private float coolDownG = 15;
    private bool isActiveAbG = true;

    ThrowBalloon ThrowBalloonScript;

    //shooting
    public GameObject shootPrefab;
    private float shootForce = 10;
    private GameObject tmpShoot;
    public Transform pointToShoot;
    public ParticleSystem bubbleBean;


    public GameObject playeUIObj;
    PlayerUI playerUIScript;
    PlayerHealth playerHealthScript;

    //items
    private int waterBottle = 1;
    private int waterGlobes = 1;
    private int itemFeather = 0;

    public Text feathersText;
    public Text bottleText;
    public Text globeText;
    public Text totalFeathers;
    public Text totalGlobes;
    public Text totalBottles;

    // audio attacks
    public AudioSource sourceShoot;
    public AudioClip clipShootWater;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        ThrowBalloonScript = this.gameObject.GetComponent<ThrowBalloon>();
        playerUIScript = playeUIObj.GetComponent<PlayerUI>();
        playerHealthScript = this.gameObject.GetComponent<PlayerHealth>();
        bottleText.text = waterBottle.ToString();
        feathersText.text = itemFeather.ToString();
        globeText.text = waterGlobes.ToString();
        totalFeathers.text = itemFeather.ToString();
        totalGlobes.text = waterGlobes.ToString();
        totalBottles.text = waterBottle.ToString();
    }
    void Update()
    {
        GatherInput();
        Look();
        // Move();
        MovingAnim();
        ClickMouse();
        CastAbilities();
        ActiveGun();
        // UpdateMoveCharacter();
        // aplyRot();
        ResetCooldownE();
        ResetCooldownQ();
        ResetCooldownG();
        if (setColliderHeight){
            playerCapsuleCollider.height = anim.GetFloat("HeightCollider");
        }
    }
    void FixedUpdate(){
        Move();
    }

    void GatherInput() {
        if (anim.GetBool("CanMove")){ //anim.GetBool("CanMove")
            _input = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
            if (_input.magnitude > 1) {
                magnitudeMove = 1;
            } else {
                magnitudeMove = _input.magnitude;
            }
        } else {
            _input = Vector3.zero;
            magnitudeMove = 0;
        }
    }
    void Look() {
        if ( _input != Vector3.zero){
            var relative = (transform.position + _input) - transform.position;
            var rot = Quaternion.LookRotation(relative,Vector3.up);
            transform.rotation = rot; //makes the turn instantly

            //this way makes the turn slowly
            // transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);
        }
    }
    void Move() {

        _rb.MovePosition( transform.position + (transform.forward * magnitudeMove) * _speed * Time.deltaTime);   
        // _rb.velocity = ( (transform.forward * magnitudeMove) * _speed * Time.deltaTime);   
        
    }

    void MovingAnim(){
        if (_input != Vector3.zero) {
            if (isActiveGun) {
                anim.SetBool("ArmedMoving", true);
            } else {
                anim.SetBool("Moving", true);
            }
        } else {
            anim.SetBool("Moving", false);
            anim.SetBool("ArmedMoving", false);
        }
    }

    void CastAbilities() {
        if (playerUIScript.activeAbilityQ == true) {
            if (Input.GetMouseButtonDown(0)){
                startCoolDownQ = 0;
                isActiveAbQ = false;
                ApplyMousePositionRotation();
                anim.SetBool("Moving", false);
                anim.SetBool("CanMove", false);
                // anim.SetTrigger("Shooting");
                anim.SetBool("isShooting", true);
                playerUIScript.activeAbilityQ = false;
                playerUIScript.arrow.SetActive(false);
            }
        }
        if (playerUIScript.activeAbilityE == true) {
            if (Input.GetMouseButtonDown(0)){
                startCoolDownE = 0;
                isActiveAbE = false;
                anim.SetBool("Moving", false);
                anim.SetBool("CanMove", false);
                ApplyMousePositionRotation();
                //anim.SetTrigger("Dive");
                anim.SetBool("Diving", true);
                playerUIScript.activeAbilityE = false;
                playerUIScript.arrow.SetActive(false);
            }
        }
        if (playerUIScript.activeAbilityR == true) {
            if (Input.GetMouseButtonDown(0)) {
                ApplyMousePositionRotation();
                ThrowingBalloon(getMousePos());
                anim.SetBool("Moving", false);
                anim.SetBool("CanMove", false);
                //anim.SetTrigger("Throwing");
                anim.SetBool("isThrowing", true);
                playerUIScript.activeAbilityR = false;
                playerUIScript.targetPoint.SetActive(false);
                UsedWatterGlobe();
            }
        }
    }

    void ClickMouse(){
        if (anim.GetBool("CanMove")){
            if (Input.GetKeyDown("q") && isActiveGun && !anim.GetCurrentAnimatorStateInfo(1).IsName("Gunplay 1") && isActiveAbQ) { 

                    playerUIScript.activeAbilityQ = true;
                    playerUIScript.arrow.SetActive(true);
                    /*startCoolDownQ = 0;
                    isActiveAbQ = false;
                    ApplyMousePositionRotation();
                    anim.SetBool("Moving", false);
                    anim.SetBool("CanMove", false);
                    // anim.SetTrigger("Shooting");
                    anim.SetBool("isShooting", true);*/
              
                
                // canDoAbility = false;
                //TimeDelayQ = 10;
            }
            if (Input.GetKeyDown("e") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Stand To Roll") && isActiveAbE) { // Input.GetMouseButtonDown(0)
                // Vector3 mousePosition = new Vector3(0,0,0);
                    
                    playerUIScript.activeAbilityE = true;
                    playerUIScript.arrow.SetActive(true);

                    /*startCoolDownE = 0;
                    isActiveAbE = false;
                    anim.SetBool("Moving", false);
                    anim.SetBool("CanMove", false);
                    ApplyMousePositionRotation();
                    //anim.SetTrigger("Dive");
                    anim.SetBool("Diving", true);*/
                
                
                // canDoAbility = false;
                // anim.SetBool("isDive", true);
                // Debug.Log("mouse position " + Input.mousePosition);
                // Debug.Log("position: " + cam.ScreenToWorldPoint(Input.mousePosition));
            }
            if (Input.GetKeyDown("g") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Drinking") && isActiveAbG && playerHealthScript.health < 100 && waterBottle > 0) {
                
                    startCoolDownG = 0;
                    isActiveAbG = false;
                    anim.SetBool("Moving", false);
                    anim.SetBool("CanMove", false);
                    //anim.SetTrigger("Healing");
                    anim.SetBool("isHealing", true);
                    playerHealthScript.StartHealing();
                    UsedWatterBottle();
                
            }
            if (Input.GetKeyDown("r") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Toss Grenade 1") && waterGlobes > 0) { 

                    playerUIScript.activeAbilityR = true;
                    playerUIScript.targetPoint.SetActive(true);

                    /*ApplyMousePositionRotation();
                    ThrowingBalloon(getMousePos());
                    anim.SetBool("Moving", false);
                    anim.SetBool("CanMove", false);
                    //anim.SetTrigger("Throwing");
                    anim.SetBool("isThrowing", true);*/
                
                // canDoAbility = false;
                //TimeDelayQ = 10;
            }
       }
    }

    void ApplyMousePositionRotation() {
        Ray rayClick = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 mPos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Physics.Raycast(rayClick, out RaycastHit raycastHit, Mathf.Infinity, groundMask)){
            Vector3 pointClicked = raycastHit.point;
            Debug.Log("position click: " + pointClicked);
            var relative = (new Vector3(pointClicked.x, 0, pointClicked.z)) - (new Vector3(transform.position.x, 0, transform.position.z));
            var rot = Quaternion.LookRotation(relative,Vector3.up);
            // _input = Vector3.zero;
            transform.rotation = rot; //makes the turn instantly
        }
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag.Equals("PlayerGun")) {
            if (Input.GetKey("f")) {
                gotGun = true;
                other.gameObject.SetActive(false);
            }
            //    exit.SetActive(true);
            // this.gameObject.SetActive(false);
        }
         if (other.gameObject.tag.Equals("ItemFeather")) {
            if (Input.GetKey("f")) {
                AddItemFeather();
                Destroy(other.gameObject);
            }
        }
    }

    public void ActiveGun(){
        if (gotGun) {
            if (Input.GetKeyUp("c")) {
                isActiveGun = !isActiveGun;
                Gun.SetActive(isActiveGun);
            }
        }
    }

    public void CanMoveCharacter(){
        anim.SetBool("CanMove", true);
        anim.SetBool("Diving", false);
        anim.SetBool("isThrowing", false);
        anim.SetBool("isHealing", false);
        anim.SetBool("isShooting", false);
    }

    public void EnableChangeColliderHeight() {
        setColliderHeight = true;
    }
    public void DisableChangeColliderHeight() {
        setColliderHeight = false;
    }

    public void ResetCooldownE() {
        if (startCoolDownE != -1){
            // Debug.Log(startCoolDownE);
            startCoolDownE += 1 * Time.deltaTime;
            if (startCoolDownE > coolDownE){
                isActiveAbE = true;
                startCoolDownE = -1;
            }
        }
    }
    public void ResetCooldownQ() {
        if (startCoolDownQ != -1){
            // Debug.Log(startCoolDownE);
            startCoolDownQ += 1 * Time.deltaTime;
            if (startCoolDownQ > coolDownQ){
                isActiveAbQ = true;
                startCoolDownQ = -1;
            }
        }
    }

    public void ResetCooldownG() {
        if (startCoolDownG != -1) {
            startCoolDownG += 1 * Time.deltaTime;
            if (startCoolDownG > coolDownG) {
                isActiveAbG = true;
                startCoolDownG = -1;
            }
        }
    }

    public void ThrowingBalloon(Vector3 position) {
        ThrowBalloonScript.setPositionObjective(position);
    }

    public Vector3 getMousePos(){
        Vector3 pos = new Vector3 (0,0,0);
        Ray rayClick = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 mPos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Physics.Raycast(rayClick, out RaycastHit raycastHit, Mathf.Infinity, groundMask)){
            pos = raycastHit.point;
        }
        return pos;
    }

    public void ShootWater(){
        // Vector3 directionFB = Vector3.Normalize( new Vector3(player.position.x - pointToShoot.position.x, 0, player.position.z - pointToShoot.position.z) );
        Vector3 directionWater = pointToShoot.forward;
        tmpShoot = Instantiate(shootPrefab, pointToShoot.position, Quaternion.identity);
        tmpShoot.transform.forward = directionWater;
        tmpShoot.GetComponent<Rigidbody>().AddForce(directionWater *
                                                    shootForce, 
                                                    ForceMode.Impulse);
    }

    private void UsedWatterBottle () {
        waterBottle -= 1;
        bottleText.text = waterBottle.ToString();
        totalBottles.text = waterBottle.ToString();
    }
    private void AddWatterBottle () {
        waterBottle += 1;
        bottleText.text = waterBottle.ToString();
        totalBottles.text = waterBottle.ToString();
    }
    private void UsedWatterGlobe () {
        waterGlobes -= 1;
        globeText.text = waterGlobes.ToString();
        totalGlobes.text = waterGlobes.ToString();
    }
    private void AddWatterGlobe () {
        waterGlobes += 1;
        globeText.text = waterGlobes.ToString();
        totalGlobes.text = waterGlobes.ToString();
    }
    private void AddItemFeather () {
        itemFeather += 1;
        feathersText.text = itemFeather.ToString();
        totalFeathers.text = itemFeather.ToString();
    }

    public void BuyGlobe () {
        if (itemFeather >= 3) {
            itemFeather -= 3;
            feathersText.text = itemFeather.ToString();
            totalFeathers.text = itemFeather.ToString();
            AddWatterGlobe();
        }
    }

    public void BuyBottle() {
        if (itemFeather >= 5) {
            itemFeather -= 5;
            feathersText.text = itemFeather.ToString();
            totalFeathers.text = itemFeather.ToString();
            AddWatterBottle();
        }
    }

    public void ShootBubbleBean(){
        // Vector3 directionFB = Vector3.Normalize( new Vector3(player.position.x - pointToShoot.position.x, 0, player.position.z - pointToShoot.position.z) );
        // Vector3 directionWater = pointToShoot.forward;
        // tmpShoot = Instantiate(shootPrefab, pointToShoot.position, Quaternion.identity);
        // tmpShoot.transform.forward = directionWater;
        // tmpShoot.GetComponent<Rigidbody>().AddForce(directionWater *
        //                                            shootForce, 
        //                                            ForceMode.Impulse);
        bubbleBean.Play();
        sourceShoot.PlayOneShot(clipShootWater);
    }
}
    // Update is called once per frame

