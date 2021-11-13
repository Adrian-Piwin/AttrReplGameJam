using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float rotateSpeed = 5f; // Speed to look at mouse
    [SerializeField] private float moveSpeed = 5f; // Speed to reach max vel
    [SerializeField] private float maxVel; // Max achievable velocity
    [SerializeField] private float dragThreshold; // Threshold of vel speed to enable drag
    [SerializeField] private float drag; // Counter movement

    // Private
    private Rigidbody2D body;
    private PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update(){

        rotatePlayer();
    }

    private void FixedUpdate() {
        movePlayer();
    }

    // Move player
    private void movePlayer(){
        // Movement
        if (body.velocity.magnitude < maxVel)
            body.AddForce(playerInput.movementInput * moveSpeed);

        // Drag
        if (body.velocity.magnitude > dragThreshold)
        {
            body.velocity = new Vector2(body.velocity.x * drag, body.velocity.y * drag);
        }
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
        Vector3 worldPoint;
        // Face player up on start of game
        if (playerInput.mousePosition == Vector3.zero)
            worldPoint = transform.up;
        else
            worldPoint = Camera.main.ScreenToWorldPoint(playerInput.mousePosition);

        Vector2 dir = new Vector2(worldPoint.x - transform.position.x, worldPoint.y - transform.position.y);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
    }
}
