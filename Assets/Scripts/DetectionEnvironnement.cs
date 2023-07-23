using System;
using System.Collections.Generic;
using UnityEngine;

public class DetectionEnvironnement : MonoBehaviour
{
    [SerializeField] private CircleData _circleData;

    // faire via appel de fonction
    private void Update()
    {
        DetectElement();
    }

    void DetectElement()
    {
        List<Vector3> dir = new List<Vector3>();
        dir = _circleData.CirclesPoints(false);

        for (int i = 0; i < dir.Count; i++)
        {
            RaycastHit hit;

            Debug.DrawRay(transform.position, dir[i]* _circleData._radius, Color.green);
            
            if (Physics.Raycast(transform.position, dir[i], out hit, _circleData._radius))
            {
                
            }
        }
    }
}
