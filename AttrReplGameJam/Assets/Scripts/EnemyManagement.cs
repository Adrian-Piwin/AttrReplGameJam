using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagement : MonoBehaviour
{
    [SerializeField] private float spawnInterval;
    [SerializeField] private float spawnBuffer;

    [SerializeField] List<GameObject> enemies;

    private Camera cam;
    private float distanceZ, leftConstraint, rightConstraint, bottomConstraint, topConstraint;
    private List<bool> spawnSide; // Used to know what sides a enemy spawned in last
    private IEnumerator spawner;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        distanceZ = Mathf.Abs(cam.transform.position.z + transform.position.z);

        leftConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).x;
        rightConstraint = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, distanceZ)).x;
        bottomConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).y;
        topConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, distanceZ)).y;

        spawnSide = new List<bool>(new bool[4]);
        spawner = EnemySpawner();
    }

    // Enable spawning enemies
    public void EnableSpawning() 
    {
        StartCoroutine(spawner);
    }

    // Disable spawning enemies
    public void DisableSpawning() 
    {
        StopCoroutine(spawner);
    }

    // Spawn enemies with interval
    IEnumerator EnemySpawner() 
    {
        while (true) 
        {
            GameObject enemy = Instantiate(SelectEnemy(), GetSpawnPoint(), Quaternion.identity);
            EnableEnemyWrapping(enemy);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Enable wrapping after time to not mess with spawning
    IEnumerator EnableEnemyWrapping(GameObject enemy)
    { 
        enemy.GetComponent<ScreenWrap>().canWrap = false;
        yield return new WaitForSeconds(1f);
        enemy.GetComponent<ScreenWrap>().canWrap = true;
    }

    // Select enemy to spawn
    private GameObject SelectEnemy() 
    {
        return enemies[Random.Range(0, enemies.Count)];
    }

    // Get offscreen position to spawn enemy
    private Vector2 GetSpawnPoint() 
    {
        // Find random wall that has not spawned an enemy yet
        List<int> availableSpawnSide = new List<int>();
        for (int i = 0; i < spawnSide.Count; i++) 
        {
            if (spawnSide[i] == false)
                availableSpawnSide.Add(i);
        }
        if (availableSpawnSide.Count == 0) 
        {
            for (int i = 0; i < spawnSide.Count; i++)
            {
                spawnSide[i] = false;
                availableSpawnSide.Add(i);
            }
        }

        // Get random x and y position off the screen
        int randomSide = Random.Range(0, availableSpawnSide.Count);
        float xPos = 0, yPos = 0;
        switch (availableSpawnSide[randomSide]) 
        {
            case 0:
                yPos = topConstraint + spawnBuffer;
                break;
            case 1:
                yPos = bottomConstraint - spawnBuffer;
                break;
            case 2:
                xPos = rightConstraint + spawnBuffer;
                break;
            case 3:
                xPos = leftConstraint - spawnBuffer;
                break;
        }

        if (xPos == 0)
            xPos = Random.Range(leftConstraint, rightConstraint);
        else if (yPos == 0)
            yPos = Random.Range(bottomConstraint, topConstraint);

        return new Vector2(xPos, yPos);
    }
}
