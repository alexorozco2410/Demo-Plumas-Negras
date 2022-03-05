using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] controlPoints;
    private NavMeshAgent agent;
    private Animator enemyAnimator;
    private Transform player;
    private Transform currentPatrolPoint;
    private EnemyState currentState;
    public float distanceToChase = 5;
    public float distanceToAttack = 2;
    private float currentDistanceToPlayer;
    public GameObject fireBallPrefab;
    private float fireBallForce = 10;
    private GameObject tmpFireBall;
    public Transform pointToShoot;
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // InvokeRepeating("ApplyPatrolDestination", 0f, 8f);
        currentState = EnemyState.PATROL; // estado inicial de la maquina de estados
        // ApplyPatrolDestination();
        ExecutePatrol();
        CalculateDistanceToPlayer();
        // agent.SetDestination(controlPoints[Random.Range(0, controlPoints.Length)].position);
    }

    void Update() {
        CheckDistancePatrolPoint();
        CalculateDistanceToPlayer();
        EnemyFSM();
    }

    public void ApplyPatrolDestination() {
        currentPatrolPoint = controlPoints[Random.Range(0, controlPoints.Length)];
        agent.SetDestination(currentPatrolPoint.position);
    }

    public void CheckDistancePatrolPoint() {
        float currentDistance = (currentPatrolPoint.position - this.transform.position).magnitude;
        if (currentDistance <= 1.5f) {
            ApplyPatrolDestination();
        }
    }

    public void CalculateDistanceToPlayer() {
        currentDistanceToPlayer = (player.position - this.transform.position).magnitude;
    }

    private void EnemyFSM(){
        switch(currentState){
            case EnemyState.PATROL:
                if (currentDistanceToPlayer <= distanceToChase) {
                    currentState = EnemyState.CHASE;
                    ExecuteChase();
                }
                break;
            case EnemyState.CHASE:
                if (agent.remainingDistance <= distanceToAttack) {
                    currentState = EnemyState.ATTACK;
                    ExecuteAttack();
                } else if (agent.remainingDistance > distanceToChase){
                    currentState = EnemyState.PATROL;
                    ExecutePatrol();
                }
                break;
            case EnemyState.ATTACK:
                if (currentDistanceToPlayer > distanceToAttack) {
                    enemyAnimator.SetBool("EnemyAttack", false);
                    currentState = EnemyState.CHASE;
                    ExecuteChase();
                }
                break;
        }
    }

    //metodos para la maquina de estados
    private void ExecutePatrol(){
        ApplyPatrolDestination();
    }

    private void ExecuteChase(){
        agent.SetDestination(player.position);
    }
    private void ExecuteAttack(){
        /*var relative = (new Vector3(player.position.x, 0, player.position.z)) - (new Vector3(transform.position.x, 0, transform.position.z));
        var rot = Quaternion.LookRotation(relative,Vector3.up);
        // _input = Vector3.zero;
        //transform.rotation = rot;
        transform.rotation = rot; */
        enemyAnimator.SetBool("EnemyAttack", true);
    }

    //ataque de enemigo
    public void ShootFireBall(){
        // Vector3 directionFB = Vector3.Normalize( new Vector3(player.position.x - pointToShoot.position.x, 0, player.position.z - pointToShoot.position.z) );
        Vector3 directionFB = pointToShoot.forward;
        tmpFireBall = Instantiate(fireBallPrefab, pointToShoot.position, Quaternion.identity);
        tmpFireBall.transform.forward = directionFB;
        tmpFireBall.GetComponent<Rigidbody>().AddForce(directionFB *
                                                    fireBallForce, 
                                                    ForceMode.Impulse);
    }
}

public enum EnemyState { PATROL, CHASE, ATTACK }; //FSM STATES
