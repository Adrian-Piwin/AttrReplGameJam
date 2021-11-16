using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [System.NonSerialized] public Vector2 movementInput;
    [System.NonSerialized] public Vector3 mousePosition;
    [System.NonSerialized] public bool fireInput;
    [System.NonSerialized] public bool fireInputDown;

    [System.NonSerialized] private bool takingInput;

    public void EnableInput()
    {
        takingInput = true;
    }

    public void DisableInput() 
    {
        takingInput = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!takingInput) return;

        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        mousePosition = Input.mousePosition;

        fireInput = Input.GetButton("Fire2");
        fireInputDown = Input.GetButtonDown("Fire1");
    }
}
