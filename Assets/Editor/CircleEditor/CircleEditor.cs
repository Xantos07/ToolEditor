using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ToolEditor
{
    [CustomPropertyDrawer(typeof(CircleData))]
    public class CircleEditor : PropertyDrawer
    {
        private int graphSize = 330;
        private CircleData circleData;
        void UpdateGraph(SerializedProperty property, Rect position)
        {
            List<Vector3> points = circleData.HalfCirclePoints(Orientation.XY, true);
            
            float circleHeight = (graphSize / 2) * 0.40f;
            Vector3 center = new Vector3(position.center.x, position.center.y +circleHeight, 0);

            Handles.color = circleData.ColorDetection;
            Vector3 pointA = new Vector3();
            Vector3 pointB = new Vector3();

            for (int i = 0; i < points.Count; i++)
            {
                 pointA = points[i] * 80f;
                 pointB = new Vector3(points[i].x, -points[i].y, 0) * 80f;

                if (i + 1 < points.Count)
                {
                    Vector3  pointANext = points[i + 1] * 80f;
                    Vector3  pointBNext = new Vector3(points[i + 1].x, -points[i + 1].y, 0) * 80f;
                    
                    DrawTriangle(center, pointA, pointANext);
                    DrawTriangle(center, pointB, pointBNext);
                }
            }

            DrawPawn(center, 10f, circleData.ColorPawn);
        }

        void DrawTriangle(Vector3 center, Vector3 pointA, Vector3 nextPointA)
        {
            Handles.DrawAAConvexPolygon(center, pointA +center, nextPointA + center);
        }
        
        void DrawPawn(Vector3 center, float radius, Color color)
        {
            Handles.color = color;
            Handles.DrawSolidDisc(center, Vector3.forward, radius);
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {       
            Rect propertyRect = EditorGUILayout.GetControlRect(false,  graphSize);
            EditorGUI.DrawRect(propertyRect, new Color32(45, 45, 45, 255));
            
            EditorGUI.PropertyField(propertyRect, property, true);

            EditorGUI.BeginProperty(position, label, property);

            circleData = (CircleData) fieldInfo.GetValue(property.serializedObject.targetObject);

            UpdateGraph(property, propertyRect);

            EditorGUI.EndProperty();
        }
    }
}