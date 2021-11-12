using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStarController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float chainTime; // Time between kills to be a chain

    [Header("Star Movement Settings")]
    [SerializeField] private float attractForce; // Force of attraction
    [SerializeField] private float repelForce; // Force of repel
    [SerializeField] private float repelTimeBuffer; // Time to wait after repelling to allow attraction
    [SerializeField] private float drag; // Star drag
    [SerializeField] private float dragThreshold; // Star vel threshold to enable drag

    [Header("References")]
    [SerializeField] private Transform attractPoint;
    [SerializeField] private GameObject playerStar;
    [SerializeField] private GameObject playerStarPrefab;
    [SerializeField] private AttractionBeam attractionBeam;

    // Actions
    public Action OnRepelStar;
    public Action OnAttractStar;
    public Action OnStopAttractStar;
    public Action OnStarReturn;
    public Action OnStarHit;

    private GameObject star;
    private float timeSinceLastKill;
    private bool fireInput;
    private bool fireInputDown;
    private bool allowAttraction;
    private bool isStarConnected = true;
    private bool isAttracting;

    // Update is called once per frame
    void Update()
    {
        fireInput = Input.GetButton("Fire1");
        fireInputDown = Input.GetButtonDown("Fire1");
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
        if (!fireInputDown || !isStarConnected) return;

        //OnRepelStar();

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
        // Subscribe to star functions
        starScript.OnHitEnemy += HitEnemy;
        starScript.OnHitPlayer += StarRetrieved;

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
        if (!fireInput || isStarConnected || !allowAttraction) 
        {
            // Disable beam
            attractionBeam.DisableBeam();
            if (isAttracting)
            {
                //OnStopAttractStar();
                isAttracting = false;
            }
            return;
        }
        if (!isAttracting)
        {
            //OnAttractStar();
            isAttracting = true;
        }

        // Get position of halfway between star and facing direction
        Vector3 targetPos = attractPoint.transform.position + (transform.up * (Vector2.Distance(star.transform.position, attractPoint.transform.position) / 2));

        // Enable beam
        attractionBeam.SetBeamPos(attractPoint.transform.position, targetPos, star.transform.position);

        Rigidbody2D starBody = star.GetComponent<Rigidbody2D>();
        starBody.velocity = (targetPos - star.transform.position).normalized * attractForce;
    }

    // Star Retrieved
    private void StarRetrieved() 
    {
        if (!allowAttraction) return;

        //OnStarReturn();

        // Destroy prefab star
        Destroy(star);

        // Disable star on player
        playerStar.SetActive(true);

        isStarConnected = true;
    }

    // Star hit enemy
    private void HitEnemy() 
    {
        OnStarHit();

        bool isChained = false;
        if (Time.time - timeSinceLastKill < chainTime)
            isChained = true;
        timeSinceLastKill = Time.time;

        Enemy enemy = star.GetComponent<Star>().enemy.GetComponent<Enemy>();
        enemy.TakeDamage(isChained);
    }

    // Time to wait before allowing attraction
    IEnumerator RepelTime() 
    {
        allowAttraction = false;
        yield return new WaitForSeconds(repelTimeBuffer);
        allowAttraction = true;
    }
}
