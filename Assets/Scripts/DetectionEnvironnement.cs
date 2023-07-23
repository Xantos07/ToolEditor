using System.Collections.Generic;
using UnityEngine;

public class DetectionEnvironnement : MonoBehaviour
{
    [SerializeField] private CircleData circleData;
    
    void DetectElement()
    {
        List<Vector3> dir = new List<Vector3>();
        dir = circleData.CirclesPoints(false);

        for (int i = 0; i < dir.Count; i++)
        {
            RaycastHit hit;

            Debug.DrawRay(transform.position, dir[i]* circleData.Radius, Color.green);
            
            if (Physics.Raycast(transform.position, dir[i], out hit, circleData.Radius))
            {
                
            }
        }
    }
}
