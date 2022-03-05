using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100;
    public float maxHealth;
    public bool noTakeDamage = false;
    public float timeNoTakeDamage = 1f;
    public Image healthBar;
    
    void Start() {
        maxHealth = health;
    }

    void Update() {
        // CheckHealth();
    }
    
    public void TakeDamage( int damage ) {
        if (health > 0 && !noTakeDamage) {
            health -= damage;
            healthBar.fillAmount = health/maxHealth;
            StartCoroutine(NoTakeDamage());
            Debug.Log("player life: " + health);
        }
    }

    IEnumerator NoTakeDamage(){
        noTakeDamage = true;
        yield return new WaitForSeconds(timeNoTakeDamage);
        noTakeDamage = false;
    }

    public void CheckHealth(){
        if (health > 0){
            healthBar.fillAmount = health/maxHealth;
        } else {
            healthBar.fillAmount = 0;
        }
    }
}
