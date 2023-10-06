using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(CircleData))]
public class CicleEditor : PropertyDrawer
{
    public int GraphSize = 300;
    private CircleData _circleData;

    private const float _radius = 80f;
    private const float _heightPourcentage = 0.40f;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect propertyRect = EditorGUILayout.GetControlRect(false, GraphSize);
        EditorGUI.DrawRect(propertyRect, new Color32(45,45,45, 255));

        EditorGUI.PropertyField(propertyRect, property, true);
        
        EditorGUI.BeginProperty(position, label, property);

        _circleData = (CircleData) fieldInfo.GetValue(property.serializedObject.targetObject);

        UpdateGraph(property, propertyRect);
        
        EditorGUI.EndProperty();
    }

    void UpdateGraph(SerializedProperty property, Rect position)
    {
        List<Vector3> points = _circleData.HalfCirclePoints(Orientation.XY, true);

        float circleHeight = (GraphSize / 2) * _heightPourcentage;
        Vector3 center = new Vector3(position.center.x, position.center.y + circleHeight, 0);
        
        Handles.color = _circleData.colorDetection;
        Vector3 pointA = new Vector3();
        Vector3 pointB = new Vector3();

        for (int i = 0; i < points.Count; i++)
        {
            pointA = points[i] * _radius;
            pointB = new Vector3(points[i].x, -points[i].y, 0) * _radius;
    
            if(i+1 < points.Count)
            {
                Vector3 pointANext = points[i + 1] * _radius;
                Vector3 pointBNext = new Vector3(points[i + 1].x, -points[i + 1].y, 0) * _radius;

                DrawTriangle(center, pointA, pointANext);
                DrawTriangle(center, pointB, pointBNext);
            }
        }

        DrawPawn(center, 10f, _circleData.colorPawn);
    }
    void DrawPawn(Vector3 center, float radius, Color color)
    {
        Handles.color = color;
        Handles.DrawSolidDisc(center, Vector3.forward, radius);
    }
    void DrawTriangle(Vector3 center, Vector3 pointA, Vector3 nextPointA)
    {
        Handles.DrawAAConvexPolygon(center, pointA + center, nextPointA + center);
    }
}
