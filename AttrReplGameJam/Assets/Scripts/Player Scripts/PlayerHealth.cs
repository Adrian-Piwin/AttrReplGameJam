using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int health;
    [SerializeField] private float invincibleTime; // Time to be invincible after hit

    [Header("References")]
    [SerializeField] private GameObject deathEffect;

    private int currentHealth;
    private bool isInvincible;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;

        Actions.OnEnemyHitPlayer += TakeDamage;
    }

    private void TakeDamage()
    {
        if (isInvincible) return;

        currentHealth--;

        // Die or take damage
        if (currentHealth == 0)
            Die();
        else
        {
            StartCoroutine(InvincibleTime());
        }
    }

    IEnumerator InvincibleTime()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }

    private void Die()
    {
        Actions.OnPlayerDeath();
        Destroy(Instantiate(deathEffect, transform.position, Quaternion.identity), 2f);
        Destroy(gameObject);
    }

    void OnDestroy() 
    {
        Actions.OnEnemyHitPlayer -= TakeDamage;
    }
}
