using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Settings")]
    public int health;
    public int pointValue;

    [Header("Movement Settings")]
    public float rotateSpeed = 5f; // Speed to look at mouse
    public float moveSpeed = 5f; // Speed to reach max vel
    public float maxVel; // Max achievable velocity
    public float drag; // Counter movement

    [Header("Effects")]
    public GameObject deathEffect; // Death particle effect

    [Header("References")]
    public HealthBar healthbar;
    public Rigidbody2D body;

    // Player variables
    private Transform player;
    private bool isPlayerDead;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        currentHealth = health;

        Actions.OnPlayerDeath += PlayerDied;
    }

    private void FixedUpdate()
    {
        if (isPlayerDead) return;

        Move();
        Orientation();
    }

    // Stop attacking if player is dead
    private void PlayerDied() 
    {
        isPlayerDead = true;
    }

    // Move towards player by default
    public virtual void Move() 
    {
        // Movement
        if (body.velocity.magnitude < maxVel)
            body.AddForce((player.transform.position - transform.position).normalized * moveSpeed);

        // Drag
        body.velocity = new Vector2(body.velocity.x * drag, body.velocity.y * drag);
    }

    // Face player by default
    public virtual void Orientation() 
    {
        // Look at player
        Vector2 dir = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
    }

    // Take damage from player
    public void TakeDamage() 
    {
        // Take damage
        currentHealth--;
        if (currentHealth == 0)
            Die();
        else
            healthbar.TakeDamage((float)currentHealth / (float)health);
    }

    // Destroy self and spawn effects on death
    private void Die()
    {
        // Destroy effect
        Destroy(Instantiate(deathEffect, transform.position, Quaternion.identity), 1f);

        // Give position and point value to action
        Actions.OnEnemyDeath(transform.position, pointValue);

        Destroy(transform.parent.gameObject);
    }

    // Damage player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // On hit player
        if (collision.gameObject.tag == "Player")
            Actions.OnEnemyHitPlayer();
    }

    void OnDestroy() 
    {
        Actions.OnPlayerDeath -= PlayerDied;
    }
}
