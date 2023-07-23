using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(CircleData))]
public class CircleEditor : PropertyDrawer
{
    private int graphSize = 300;
    private CircleData _secondOrder;
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return (EditorGUIUtility.singleLineHeight + 2) * 5 + graphSize;
    }
    
    public void updateGraph(SerializedProperty property,Rect position)
    {
        _secondOrder = (CircleData) fieldInfo.GetValue(property.serializedObject.targetObject);
        
        List<Vector3> points = _secondOrder.CirclesPoints(true);
        
         Rect center =  new Rect(position.x, position.y + 3 * (EditorGUIUtility.singleLineHeight + 2), position.width, graphSize);
         Vector3 posCenter = new Vector3(center.center.x, center.center.y + 25f, 0);

         for (int i = 0; i < points.Count; i++)
        {
            Handles.color = _secondOrder.ColorDetection;
            Vector3 pointA = points[i] * 100f;
            Vector3 pointB = new Vector3(points[i].x,-points[i].y, 0) * 100f;
            
            if(i+1 < points.Count)
            {
                Vector3 pointAsecond = points[i + 1] * 100f;
                Handles.DrawAAConvexPolygon(posCenter,pointA+ posCenter, pointAsecond+ posCenter);
                
                Vector3 pointBsecond = new Vector3(points[i + 1].x,-points[i +1].y, 0) * 100f;
                Handles.DrawAAConvexPolygon(posCenter,pointB + posCenter, pointBsecond+ posCenter);
            }
            
            Handles.color = _secondOrder.ColorPawn;
            Handles.DrawSolidDisc(posCenter, Vector3.forward, 10f);
        }
    }

    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        _secondOrder = (CircleData) fieldInfo.GetValue(property.serializedObject.targetObject);
        
        // Calculate rects
        var angle = new Rect(position.x, position.y+ (EditorGUIUtility.singleLineHeight + 2), position.width, EditorGUIUtility.singleLineHeight);
        var triangleCount = new Rect(position.x, position.y + (EditorGUIUtility.singleLineHeight + 2)*2, position.width, EditorGUIUtility.singleLineHeight);
        var radius = new Rect(position.x, position.y + (EditorGUIUtility.singleLineHeight + 2)*3, position.width, EditorGUIUtility.singleLineHeight);
        var colorPawn = new Rect(position.x, position.y + (EditorGUIUtility.singleLineHeight + 2) *4, position.width, EditorGUIUtility.singleLineHeight);
        var colorDetection = new Rect(position.x, position.y + (EditorGUIUtility.singleLineHeight + 2)* 5, position.width, EditorGUIUtility.singleLineHeight);

        EditorGUI.BeginChangeCheck();
        
        EditorGUI.PropertyField(angle, property.FindPropertyRelative("_angle"), new GUIContent("angle"));
        EditorGUI.PropertyField(triangleCount, property.FindPropertyRelative("_triangleCount"), new GUIContent("triangleCount"));
        EditorGUI.PropertyField(radius, property.FindPropertyRelative("_radius"), new GUIContent("radius"));
        EditorGUI.PropertyField(colorPawn, property.FindPropertyRelative("ColorPawn"), new GUIContent("colorPawn"));
        EditorGUI.PropertyField(colorDetection, property.FindPropertyRelative("ColorDetection"), new GUIContent("colorDetection"));
        
        updateGraph(property,position);
        
        EditorGUI.EndProperty();
    }
}
