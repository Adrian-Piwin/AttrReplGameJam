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
    public void SetPointValue(int points, int chain) 
    {
        if (chain > 0)
            textMesh.text = "+" + points + " (x" + chain*2 + ")";
        else
            textMesh.text = "+" + points;
        textMesh.color = chain > 0 ? specialColor : defaultColor;
    }

    // Destroy when no longer displaying
    public void OnFinishAnimation() 
    {
        Destroy(gameObject);
    }

}
