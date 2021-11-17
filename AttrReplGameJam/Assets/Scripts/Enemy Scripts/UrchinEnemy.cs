using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrchinEnemy : Enemy
{
    private Vector2 randomPos;

    public override void Move() 
    {
        // Get random position
        if (randomPos == Vector2.zero || Vector2.Distance(transform.position, randomPos) < 1f)
            randomPos = FindRandomPosition();

        // Movement
        if (body.velocity.magnitude < maxVel)
            body.AddForce((randomPos - (Vector2)transform.position).normalized * moveSpeed);

        // Drag
        body.velocity = new Vector2(body.velocity.x * drag, body.velocity.y * drag);
    }

    public override void Orientation()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }

    // Find random position on screen
    private Vector2 FindRandomPosition() 
    {
        float spawnY = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
        float spawnX = Random.Range
            (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

        return new Vector2(spawnX, spawnY);
    }
}
