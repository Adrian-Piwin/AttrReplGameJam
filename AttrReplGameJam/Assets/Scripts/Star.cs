using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [System.NonSerialized] public float drag;
    [System.NonSerialized] public float dragThreshold;
    [System.NonSerialized] public GameObject enemy;

    private Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Drag();
    }

    // Drag
    private void Drag() 
    {
        if (body.velocity.magnitude > dragThreshold) 
        {
            body.velocity = new Vector2(body.velocity.x * drag, body.velocity.y * drag);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            enemy = collision.gameObject;
            Actions.StarHitEnemy();
        }

        if (collision.transform.tag == "AttractPoint")
            Actions.StarHitPlayer();
    }
}
