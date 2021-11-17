using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    [SerializeField] private float hitStopTime;

    private bool isWaiting;

    // Start is called before the first frame update
    void Start()
    {
        Actions.OnStarHit += DoHitStop;
    }

    private void DoHitStop() 
    {
        if (isWaiting) return;

        StartCoroutine(HitStopCoroutine());
    }

    IEnumerator HitStopCoroutine() 
    {
        Time.timeScale = 0;
        isWaiting = true;
        yield return new WaitForSecondsRealtime(hitStopTime);
        Time.timeScale = 1;
        isWaiting = false;
    }

    void OnDestroy() 
    {
        Actions.OnStarHit -= DoHitStop;
    }
}
