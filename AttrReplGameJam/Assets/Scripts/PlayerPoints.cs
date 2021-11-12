using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoints : MonoBehaviour
{
    [SerializeField] private int totalPoints;

    public void AddPoints(int points) 
    {
        totalPoints += points;
    }
}
