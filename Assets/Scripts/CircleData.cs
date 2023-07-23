using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CircleData
{
    //[Range(0, Mathf.PI * 2)]
    [Range(0, 180)]
    public float _angle  = 0f;
    [Range(1, 15)]
    public int _triangleCount = 6;
    [Range(0f, 15f)]
    public float _radius = 6f;
    
    public Color ColorDetection = Color.blue;
    public Color ColorPawn = Color.red;

    public List<Vector3> CirclesPoints(bool yAxis)
    {
        float triangleAngle = (_angle * (Mathf.PI / 180))  / _triangleCount;
        
        List<Vector3> _pos = new List<Vector3>();

        for (int i = 0; i < _triangleCount + 1; i++)
        {
            Vector3 pos = new Vector3();

            if (yAxis)
                pos = new Vector3(Mathf.Cos(i * triangleAngle), Mathf.Sin(i * triangleAngle),0);
            else
                pos = new Vector3(Mathf.Cos(i * triangleAngle), 0, Mathf.Sin(i * triangleAngle));
            
            _pos.Add(pos);
        }
        return _pos;
    }
 }