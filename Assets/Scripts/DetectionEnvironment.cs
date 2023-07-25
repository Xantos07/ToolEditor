using System.Collections.Generic;
using UnityEngine;

public class DetectionEnvironment : MonoBehaviour
{
    public List<Vector3> DetectElement (CircleData circleData, bool z)
    {
        List<Vector3> dir = circleData.HalfCirclePoints(Orientation.XZ, z);
        List<Vector3> newDir = new List<Vector3>();
        var position = transform.position;
        
        foreach (Vector3 direction in dir)
        {
            RaycastHit hit;
            float currentRadius = circleData.Radius;
            
            if (Physics.Raycast(transform.position, direction, out hit, circleData.Radius))
            {
                if (hit.transform != null)
                {
                    currentRadius = Vector3.Distance(position, hit.point);
                }
            }
            
            newDir.Add(direction * currentRadius);
            Debug.DrawRay(position, direction * currentRadius, Color.green);
        }

        return newDir;
    }
}
