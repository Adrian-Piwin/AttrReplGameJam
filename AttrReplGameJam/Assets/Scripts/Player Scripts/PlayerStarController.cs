using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStarController : MonoBehaviour
{
    [Header("Star Movement Settings")]
    [SerializeField] private float attractForceMax; // Max force of attraction
    [SerializeField] private float attractDistanceMin; // min force of attraction
    [SerializeField] private float attractDistanceMax; // Max distance of attraction for min force
    [SerializeField] private float repelForce; // Force of repel
    [SerializeField] private float repelTimeBuffer; // Time to wait after repelling to allow attraction
    [SerializeField] private float drag; // Star drag
    [SerializeField] private float dragThreshold; // Star vel threshold to enable drag

    [Header("References")]
    [SerializeField] private Transform attractPoint;
    [SerializeField] private GameObject playerStar;
    [SerializeField] private GameObject playerStarPrefab;
    [SerializeField] private AttractionBeam attractionBeam;

    private PlayerInput playerInput;
    private GameObject star;
    private bool allowAttraction;
    private bool isStarConnected = true;
    private bool isAttracting;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        // Subscribe to star functions
        Actions.StarHitEnemy += HitEnemy;
        Actions.StarHitPlayer += StarRetrieved;
    }

    // Update is called once per frame
    void Update()
    {
        Repel();
    }

    private void FixedUpdate()
    {
        Attract();
    }

    // Shoot out star
    private void Repel() 
    {
        // Check for fire input or if star is currently connected
        if (!playerInput.fireInputDown || !isStarConnected) return;

        Actions.OnRepelStar?.Invoke(); ;

        // Spawn star and launch it
        star = Instantiate(playerStarPrefab);
        // Setup position
        star.transform.position = playerStar.transform.position;
        // Setup launch force
        star.GetComponent<Rigidbody2D>().AddForce(transform.up * repelForce);
        // Setup star script
        Star starScript = star.GetComponent<Star>();
        starScript.drag = drag;
        starScript.dragThreshold = dragThreshold;

        // Disable star on player
        playerStar.SetActive(false);

        // Allow attraction after certain time after repel
        StartCoroutine(RepelTime());

        isStarConnected = false;
    }

    // Retrieve star
    private void Attract() 
    {
        // Check for fire input or if star is currently connected
        if (!playerInput.fireInput || isStarConnected || !allowAttraction) 
        {
            // Disable beam
            attractionBeam.DisableBeam();
            if (isAttracting)
            {
                Actions.OnStopAttractStar?.Invoke(); ;
                isAttracting = false;
            }
            return;
        }
        if (!isAttracting)
        {
            Actions.OnAttractStar?.Invoke(); ;
            isAttracting = true;
        }

        // Get position of halfway between star and facing direction
        Vector3 targetPos = attractPoint.transform.position + (transform.up * ((Vector2.Distance(star.transform.position, attractPoint.transform.position) / 3)*2));

        // Enable beam
        attractionBeam.SetBeamPos(attractPoint.transform.position, targetPos, star.transform.position);

        Rigidbody2D starBody = star.GetComponent<Rigidbody2D>();

        // Set attract force based on distance
        float multi =  1 - (Mathf.Clamp(Vector2.Distance(attractPoint.transform.position, star.transform.position), 0, attractDistanceMax) / attractDistanceMax);
        float attractForce = (attractForceMax * multi) < attractDistanceMin ? attractDistanceMin : (attractForceMax * multi);

        starBody.velocity = (targetPos - star.transform.position).normalized * attractForce;
    }

    // Star Retrieved
    private void StarRetrieved() 
    {
        if (!allowAttraction) return;

        Actions.OnStarReturn?.Invoke();

        // Destroy prefab star
        Destroy(star);

        // Disable star on player
        playerStar.SetActive(true);

        isStarConnected = true;
    }

    // Star hit enemy
    private void HitEnemy() 
    {
        Actions.OnStarHit?.Invoke();

        Enemy enemy = star.GetComponent<Star>().enemy.GetComponent<Enemy>();
        enemy.TakeDamage();
    }

    // Time to wait before allowing attraction
    IEnumerator RepelTime() 
    {
        allowAttraction = false;
        yield return new WaitForSeconds(repelTimeBuffer);
        allowAttraction = true;
    }

    void OnDestroy()
    {
        Actions.StarHitEnemy -= HitEnemy;
        Actions.StarHitPlayer -= StarRetrieved;
    }
}
