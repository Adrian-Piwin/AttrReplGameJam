using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string enemyName;
    [SerializeField] private float health;
    [SerializeField] private int pointValue;

    [Header("Movement Settings")]
    [SerializeField] private float rotateSpeed = 5f; // Speed to look at mouse
    [SerializeField] private float moveSpeed = 5f; // Speed to reach max vel
    [SerializeField] private float maxVel; // Max achievable velocity
    [SerializeField] private float drag; // Counter movement

    [Header("Effects")]
    [SerializeField] private GameObject deathEffect; // Death particle effect
    [SerializeField] private GameObject pointPrefab; // Point effect on death

    private Transform player;
    private Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
        FacePlayer();
    }

    private void Move() 
    {
        // Movement
        if (body.velocity.magnitude < maxVel)
            body.AddForce((player.transform.position - transform.position).normalized * moveSpeed);

        // Drag
        body.velocity = new Vector2(body.velocity.x * drag, body.velocity.y * drag);
    }

    private void FacePlayer() 
    {
        // Look at mouse
        Vector2 dir = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
    }

    public void TakeDamage(bool isChained) 
    {
        // Take damage
        health--;
        if (health == 0)
            Die(isChained);
    }

    private void Die(bool isChained)
    {
        // Death effects
        GameObject pointEffect = Instantiate(pointPrefab);
        pointEffect.transform.position = transform.position;
        pointEffect.GetComponent<PointEffect>().SetPointValue(pointValue, isChained);

        // Give points to player
        player.GetComponent<PlayerPoints>().AddPoints(isChained ? pointValue * 2 : pointValue);

        Destroy(gameObject);
    }
}
