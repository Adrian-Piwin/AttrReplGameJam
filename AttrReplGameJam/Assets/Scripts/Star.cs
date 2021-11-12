using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [System.NonSerialized] public float drag;
    [System.NonSerialized] public float dragThreshold;

    private Rigidbody2D body;

    public Action OnHitEnemy;
    public Action OnHitPlayer;

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
            OnHitEnemy();

        if (collision.transform.tag == "AttractPoint")
            OnHitPlayer();
    }
}
