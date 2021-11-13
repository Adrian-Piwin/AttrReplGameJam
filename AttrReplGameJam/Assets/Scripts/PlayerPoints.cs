using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerPoints : MonoBehaviour
{
    public Action PointsAdded;
    private int totalPoints;

    public void AddPoints(int points) 
    {
        totalPoints += points;
        PointsAdded();
    }

    public int GetPoints() 
    {
        return totalPoints;
    }
}
