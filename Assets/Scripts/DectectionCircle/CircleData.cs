using System;
using System.Collections.Generic;
using UnityEngine;

namespace ToolEditor
{
    [Serializable]
    public class CircleData
    {
        [Range(0, 180)] public float Angle = 0f;
        [Range(2, 15)] public int TriangleAmount = 6;
        [Range(0f, 15f)] public float Radius = 6f;

        [Range(1, 5)] public int Fragment = 2;

        public Color ColorDetection = Color.blue;
        public Color ColorPawn = Color.red;

        /// <summary>
        /// Creating points of a half circle
        /// </summary>
        /// <param name="orientation"></param>
        /// <param name="positif"></param>
        /// <returns></returns>
        public List<Vector3> HalfCirclePoints(Orientation orientation, bool positif)
        {
            float radian = Mathf.PI / 180f; 
            float pointRad = (Angle * radian) / TriangleAmount ; 

            List<Vector3> _pos = new List<Vector3>(); 
            Vector3 newPos = new Vector3();
            
            for (int i = 0; i < (TriangleAmount +1); i++)
            {
                float cos = Mathf.Cos(i * pointRad); 
                float sin = Mathf.Sin(i * pointRad); 

                if (positif)
                    newPos = orientation == Orientation.XY ? new Vector3(cos, sin, 0) : new Vector3(cos, 0, sin);
                else
                    newPos = orientation == Orientation.XY ? new Vector3(cos, -sin, 0) : new Vector3(cos, 0, -sin);

                _pos.Add(newPos);
            }

            return _pos;
        }

        /// <summary>
        /// Creating points on different layers of a half circle 
        /// </summary>
        /// <returns></returns>
        public List<Vector3> MultipleHalfCirclePoints()
        {
            List<Vector3> positionMultiplePoints = new List<Vector3>();
            List<Vector3> positionPoints = HalfCirclePoints(Orientation.XZ, true);
            positionPoints.AddRange(HalfCirclePoints(Orientation.XZ, false));
            float dist = DistanceBtwPoint();

            foreach (var point in positionPoints)
            {
                for (int j = 0; j < Fragment; j++)
                {
                    positionMultiplePoints.Add(point * dist * (j + 1));
                }
            }

            return positionMultiplePoints;
        }

        /// <summary>
        /// Distance between points  
        /// </summary>
        /// <returns></returns>
        public float DistanceBtwPoint()
        {
            return Radius / Fragment;
        }
    }
}