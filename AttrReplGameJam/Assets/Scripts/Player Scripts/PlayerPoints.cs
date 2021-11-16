using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerPoints : MonoBehaviour
{
    private int totalPoints;

    public void AddPoints(int points) 
    {
        totalPoints += points;
        Actions.PointsAdded?.Invoke();
    }

    public int GetPoints() 
    {
        return totalPoints;
    }
}
