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
    
    void Start() {
        maxHealth = health;
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
}
