using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Settings")]
    public float rotateSpeed = 5f; // Speed to look at mouse
    public float moveSpeed = 5f; // Speed to reach max vel
    public float maxVel; // Max achievable velocity
    public float dragThreshold; // Threshold of vel speed to enable drag
    public float drag; // Counter movement

    // Private
    private Rigidbody2D body;
    private Vector2 playerInput;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        playerInput.x = Input.GetAxisRaw("Horizontal");
        playerInput.y = Input.GetAxisRaw("Vertical");

        rotatePlayer();
    }

    private void FixedUpdate() {
        movePlayer();
    }

    // Move player
    private void movePlayer(){
        // Movement
        if (body.velocity.magnitude < maxVel)
            body.AddForce(playerInput * moveSpeed);

        // Drag
        if ((body.velocity.magnitude > dragThreshold && playerInput.magnitude == 0) || body.velocity.magnitude >= maxVel)
        {
            body.velocity = new Vector2(body.velocity.x * drag, body.velocity.y * drag);
        }

        Debug.Log(body.velocity.magnitude);
    }

    /* Alt movement
    private void movePlayer()
    {
        // Movement
        body.AddForce(playerInput * moveSpeed);

        // Drag
        if (body.velocity.magnitude > dragThreshold)
        {
            body.velocity = new Vector2(body.velocity.x * drag, body.velocity.y * drag);
        }

        Debug.Log(body.velocity.magnitude);
    }*/

    // Rotate player towards mouse
    private void rotatePlayer(){
    	// Look at mouse
	    Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = new Vector2(worldPoint.x - transform.position.x, worldPoint.y - transform.position.y);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
    }
}
