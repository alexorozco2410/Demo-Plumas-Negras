using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100;
    public float maxHealth;
    public bool noTakeDamage = false;
    public float timeNoTakeDamage = 1f;
    public Image healthBar;

    IsometricCC playerControllerScript;
    public Animator playerAnimator;
    
    void Start() {
        maxHealth = health;
        playerControllerScript = this.gameObject.GetComponent<IsometricCC>();
        playerAnimator = this.gameObject.GetComponent<Animator>();
    }

    void Update() {
        // CheckHealth();
        if (health <= 0) {
            StartCoroutine(GameOver());
        }
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
            // StartCoroutine(GameOver());
        }
    }

    public void StartHealing(){
        StartCoroutine(Healing());
    }

    private void RestoreHealth() {
        if (health < 100) {
            health += 10;
            healthBar.fillAmount = health/maxHealth;
        }
    }

    IEnumerator Healing() {
        RestoreHealth();
        yield return new WaitForSeconds(1);
        RestoreHealth();
        yield return new WaitForSeconds(1);
        RestoreHealth();
        yield return new WaitForSeconds(1);
        RestoreHealth();
    }

    IEnumerator GameOver() {
        playerControllerScript.enabled = false;
        playerAnimator.SetBool("Die", true);
        yield return new WaitForSeconds(8);
        SceneManager.LoadScene(1);
    }
}
