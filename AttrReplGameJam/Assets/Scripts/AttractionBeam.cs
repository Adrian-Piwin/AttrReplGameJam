using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractionBeam : MonoBehaviour
{
    [SerializeField] private float vertexCount;

    private LineRenderer lr;
    private List<Vector3> points;
    private Vector3 point1;
    private Vector3 point2;
    private Vector3 point3;

    // Start is called before the first frame update
    void Start()
    {
        points = new List<Vector3>();
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!lr.enabled) return;

        LRCurve();
    }

    // Stop beam
    public void DisableBeam() 
    {
        lr.enabled = false;
    }
    
    // Setup beam
    public void SetBeamPos(Vector3 startPos, Vector3 middlePos, Vector3 endPos) 
    {
        lr.enabled = true;

        point1 = startPos;
        point2 = middlePos;
        point3 = endPos;
    }

    private void LRCurve() 
    {
        points.Clear();
        for (float ratio = 0; ratio <= 1; ratio += 1f / vertexCount)
        {
            var tangent1 = Vector3.Lerp(point1, point2, ratio);
            var tangent2 = Vector3.Lerp(point2, point3, ratio);
            var curve = Vector3.Lerp(tangent1, tangent2, ratio);

            points.Add(curve);
        }

        lr.positionCount = points.Count;
        lr.SetPositions(points.ToArray());
    }
}
