using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private SpriteRenderer sr;
    private bool firstDamage;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
    }

    public void TakeDamage(float percent)
    {
        // Enable health bar on taking damage
        if (!firstDamage)
        {
            sr.enabled = true;
            firstDamage = true;
        }

        StartCoroutine(ScaleToTargetCoroutine(new Vector2(percent, transform.localScale.y), speed));
    }

    private IEnumerator ScaleToTargetCoroutine(Vector2 targetScale, float duration)
    {
        Vector2 startScale = transform.localScale;
        float timer = 0.0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            //smoother step algorithm
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            transform.localScale = Vector2.Lerp(startScale, targetScale, t);
            yield return null;
        }

        yield return null;
    }
}
