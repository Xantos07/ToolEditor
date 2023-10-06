using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CircleData
{
   [Range(0f, 180f)] public float Angle = 0f;
   [Range(2,15f)] public int TriangleAmount = 6;
   [Range(0f, 50f)] public float Radius = 6f;

   public Color colorDetection;
   public Color colorPawn;
   
    public List<Vector3> HalfCirclePoints(Orientation orientation, bool halfCirclePositif)
    {
        float radian = Mathf.PI / 180f;
        float pointRad = (Angle * radian) / TriangleAmount;

        List<Vector3> pos = new List<Vector3>();
        Vector3 newPos = new Vector3();
        
        for (int i = 0; i < TriangleAmount+1; i++)
        {
            float cos = Mathf.Cos(i * pointRad);
            float sin = Mathf.Sin(i * pointRad);

            if(halfCirclePositif)
               newPos = orientation == Orientation.XY ? new Vector3(cos, sin, 0) : new Vector3(cos, 0, sin);
            else
                newPos = orientation == Orientation.XY ? new Vector3(cos, -sin, 0) : new Vector3(cos, 0, -sin);
            
            pos.Add(newPos);
        }

        return pos;
    }
}
