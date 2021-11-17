using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
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

    void OnDestroy()
    {
        Actions.OnStarHit -= DoScreenShake;
        Actions.OnEnemyHitPlayer -= DoScreenShake;
    }
}
