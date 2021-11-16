using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        Actions.OnEnemyHitPlayer += PlayerFlash;

        animator.Play("PlayerSpawn");
    }

    private void PlayerFlash() 
    {
        animator.Play("PlayerFlash");
    }
}
