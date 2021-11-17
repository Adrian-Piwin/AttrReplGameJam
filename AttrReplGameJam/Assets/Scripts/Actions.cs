using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Actions
{
    public static Action OnWarningSpawn;
    public static Action OnEnemySpawn;

    public static Action OnRepelStar;
    public static Action OnAttractStar;
    public static Action OnStopAttractStar;
    public static Action OnStarReturn;
    public static Action OnStarHit;

    public static Action<int> PointsAdded;

    public static Action StarHitEnemy;
    public static Action StarHitPlayer;

    public static Action OnEnemyHitPlayer;
    public static Action OnPlayerDeath;

    public static Action<Vector2, int> OnEnemyDeath;

}
