using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordfishEnemy : Enemy
{
    [SerializeField] private float dashChargeTime;

    [SerializeField] private Animator animator;

    private bool isCharging;

    public override void Move() 
    {
        // Drag
        body.velocity = new Vector2(body.velocity.x * drag, body.velocity.y * drag);

        // Dash movement
        if (isCharging) return;

        if (body.velocity.magnitude < maxVel)
            body.AddForce((player.transform.position - transform.position).normalized * moveSpeed, ForceMode2D.Impulse);

        StartCoroutine(Charge());
    }

    IEnumerator Charge() 
    {
        animator.Play("Charging");
        isCharging = true;
        yield return new WaitForSeconds(dashChargeTime);
        isCharging = false;
    }
}
