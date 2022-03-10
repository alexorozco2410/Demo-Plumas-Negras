using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100;
    public float maxHealth;
    public bool noTakeDamage = false;
    private float timeNoTakeDamage = 0f;

    public Image healthBar;

    public SpawnComunEnemy managerEnemies;

    public GameObject itemFeather;

    public GameObject canvasFinal;
    public Text finalText;
    
    void Start() {
        maxHealth = health;
        managerEnemies = GameObject.FindGameObjectWithTag("enemyManager").GetComponent<SpawnComunEnemy>();
    }
    void Update() {
        if (this.gameObject.tag.Equals("enemy1")) {
            CommunEnemyDeath();
        }
        if (this.gameObject.tag.Equals("finalBoss") && this.health <= 0) {
            StartCoroutine(FinalBossDeath());
        }
    }
    public void TakeDamage( float damage ) {
        if (health > 0 && !noTakeDamage) {
            health -= damage;
            healthBar.fillAmount = health/maxHealth;
            StartCoroutine(NoTakeDamage());
            Debug.Log("enemy life: " + health);
        }
    }

    IEnumerator NoTakeDamage(){
        noTakeDamage = true;
        yield return new WaitForSeconds(timeNoTakeDamage);
        noTakeDamage = false;
    }

    public void CommunEnemyDeath() {
        if (this.health <= 0) {
            // Destroy(this.gameObject);
            Vector3 currentEnemyPos = new Vector3(this.transform.position.x, 0, this.transform.position.z);
            managerEnemies.CommunEnemyDie();
            Destroy(this.gameObject);
            Instantiate(itemFeather, currentEnemyPos, Quaternion.identity);
            // Debug.Log("enemigo muerto");
        }
    }

    IEnumerator FinalBossDeath() {
        if (this.health <= 0) {
            yield return new WaitForSeconds(5);
            canvasFinal.SetActive(true);
            finalText.text = "GANASTE";
            // Destroy(this.gameObject);
            Time.timeScale = 0;
        }
    }
}
