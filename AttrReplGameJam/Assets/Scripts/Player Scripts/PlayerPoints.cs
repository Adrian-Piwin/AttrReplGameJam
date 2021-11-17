using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerPoints : MonoBehaviour
{
    [SerializeField] private float chainTime; // Time between kills to be a chain
    [SerializeField] private GameObject pointPrefab; // Time between kills to be a chain

    private int totalPoints;

    private int currentChain;
    private float timeSinceChainStart;

    private void Start()
    {
        Actions.OnEnemyDeath += EnemyKilled;
    }

    private void AddPoints(int points) 
    {
        totalPoints += points;
        Actions.PointsAdded(totalPoints);
    }

    // Spawn point effect with point value on enemy death
    private void EnemyKilled(Vector2 pos, int pointValue) 
    {
        // Add to chain if enemies killed together
        if (timeSinceChainStart + chainTime * (currentChain == 0 ? 1 : currentChain) > Time.time)
        {
            currentChain++;
        }
        // Reset chain
        else 
        {
            timeSinceChainStart = Time.time;
            currentChain = 0;
        }

        // Multiply point value by chain
        if (currentChain > 0)
            pointValue = pointValue * (2 * currentChain);

        // Spawn Point effect
        GameObject pointEffect = Instantiate(pointPrefab);
        pointEffect.transform.position = pos;
        pointEffect.GetComponent<PointEffect>().SetPointValue(pointValue, currentChain);

        // Add points
        AddPoints(pointValue);
    }

    private void OnDestroy()
    {
        Actions.OnEnemyDeath -= EnemyKilled;
    }
}
