using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FinalBossController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator enemyAnimator;
    
    public AnimatorStateInfo bossStateInfo;
    private Transform player;
    private float distanceToAttack = 3;
    private float currentDistanceToPlayer;
    public GameObject fireBallPrefab;
    private float fireBallForce = 17;
    private GameObject tmpFireBall;

    public EnemyHealth bossHealthScript;
    void Start() {
        bossHealthScript = this.gameObject.GetComponent<EnemyHealth>();
        agent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // InvokeRepeating("ApplyPatrolDestination", 0f, 8f);
        CalculateDistanceToPlayer();
        ExecuteChase();
        enemyAnimator.SetBool("isRunning", true);
        // agent.SetDestination(controlPoints[Random.Range(0, controlPoints.Length)].position);
    }

    void Update() {
        bossStateInfo = enemyAnimator.GetCurrentAnimatorStateInfo(0);
        CalculateDistanceToPlayer();
        EnemyFSM();
    }

    public void CalculateDistanceToPlayer() {
        currentDistanceToPlayer = (player.position - this.transform.position).magnitude;
    }

    private void EnemyFSM(){
        if (bossHealthScript.health > 0) {
            if (agent.remainingDistance <= distanceToAttack) {
                ExecuteAttack();
            }
            if (currentDistanceToPlayer > distanceToAttack) {
                enemyAnimator.SetBool("isAttacking", false);
                enemyAnimator.SetBool("isRunning", true);
                ExecuteChase();
            }
        } else {
                enemyAnimator.SetBool("isDying", true);
                agent.stoppingDistance = 1000;
            
        }
    }


    private void ExecuteChase(){
        agent.isStopped = false;
        agent.SetDestination(player.position);
    }

    private void ExecuteAttack(){
        /*var relative = (new Vector3(player.position.x, 0, player.position.z)) - (new Vector3(transform.position.x, 0, transform.position.z));
        var rot = Quaternion.LookRotation(relative,Vector3.up);
        // _input = Vector3.zero;
        //transform.rotation = rot;
        transform.rotation = rot; */
        agent.isStopped = true;
        enemyAnimator.SetBool("isAttacking", true);
        enemyAnimator.SetBool("isRunning", false);
        // ShootMeteor();
    }

    //ataque de enemigo
    /*public void ShootFireBall(){
        // Vector3 directionFB = Vector3.Normalize( new Vector3(player.position.x - pointToShoot.position.x, 0, player.position.z - pointToShoot.position.z) );
        Vector3 directionFB = pointToShoot.forward;
        tmpFireBall = Instantiate(fireBallPrefab, pointToShoot.position, Quaternion.identity);
        tmpFireBall.transform.forward = directionFB;
        tmpFireBall.GetComponent<Rigidbody>().AddForce(directionFB *
                                                    fireBallForce, 
                                                    ForceMode.Impulse);
    }*/

    public void ShootMeteor(){
        // Vector3 directionFB = Vector3.Normalize( new Vector3(player.position.x - pointToShoot.position.x, 0, player.position.z - pointToShoot.position.z) );
        tmpFireBall = Instantiate(fireBallPrefab, player.position + new Vector3(0, 5, 0), Quaternion.identity);
        tmpFireBall.GetComponent<Rigidbody>().AddForce(new Vector3(0, -1, 0) *
                                                    fireBallForce, 
                                                    ForceMode.Impulse);
    }
}


