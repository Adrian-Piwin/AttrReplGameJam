using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public PlayerStarController playerStarController;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Actions.OnStarHit += DoScreenShake;
        Actions.OnEnemyHitPlayer += DoScreenShake;
    }

    private void DoScreenShake() 
    {
        animator.Play("ScreenShake");
    }
}
