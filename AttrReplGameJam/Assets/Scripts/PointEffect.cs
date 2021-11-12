using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointEffect : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMesh;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color specialColor;

    // Set value, showing whether its a chain kill or not
    public void SetPointValue(int points, bool isChained) 
    {
        textMesh.text = "+" + (isChained ? points*2 : points);
        textMesh.color = isChained ? specialColor : defaultColor;
    }

    // Destroy when no longer displaying
    public void OnFinishAnimation() 
    {
        Destroy(gameObject);
    }

}
