using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ToolEditor
{
    [CustomPropertyDrawer(typeof(CircleData))]
    public class CircleEditor : PropertyDrawer
    {
        private int graphSize = 300;
        private CircleData secondOrder;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return (EditorGUIUtility.singleLineHeight + 2) * 5 + graphSize;
        }

        void UpdateGraph(SerializedProperty property, Rect position)
        {
            secondOrder = (CircleData) fieldInfo.GetValue(property.serializedObject.targetObject);

            List<Vector3> points = secondOrder.HalfCirclePoints(Orientation.XY, true);

            Rect center = new Rect(position.x, position.y, position.width, graphSize);
            Vector3 posCenter = new Vector3(center.center.x, center.center.y + 125f, 0);

            for (int i = 0; i < points.Count; i++)
            {
                Handles.color = secondOrder.ColorDetection;
                Vector3 pointA = points[i] * 100f;
                Vector3 pointB = new Vector3(points[i].x, -points[i].y, 0) * 100f;

                if (i + 1 < points.Count)
                {
                    Vector3 pointAsecond = points[i + 1] * 100f;
                    Handles.DrawAAConvexPolygon(posCenter, pointA + posCenter, pointAsecond + posCenter);

                    Vector3 pointBsecond = new Vector3(points[i + 1].x, -points[i + 1].y, 0) * 100f;
                    Handles.DrawAAConvexPolygon(posCenter, pointB + posCenter, pointBsecond + posCenter);
                }

                Handles.color = secondOrder.ColorPawn;
                Handles.DrawSolidDisc(posCenter, Vector3.forward, 10f);
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            secondOrder = (CircleData) fieldInfo.GetValue(property.serializedObject.targetObject);

            var angle = new Rect(position.x, position.y + (EditorGUIUtility.singleLineHeight + 2), position.width,
                EditorGUIUtility.singleLineHeight);
            var triangleAmount = new Rect(position.x, position.y + (EditorGUIUtility.singleLineHeight + 2) * 2,
                position.width, EditorGUIUtility.singleLineHeight);
            var radius = new Rect(position.x, position.y + (EditorGUIUtility.singleLineHeight + 2) * 3, position.width,
                EditorGUIUtility.singleLineHeight);
            var fragment = new Rect(position.x, position.y + (EditorGUIUtility.singleLineHeight + 2) * 4,
                position.width, EditorGUIUtility.singleLineHeight);
            var colorPawn = new Rect(position.x, position.y + (EditorGUIUtility.singleLineHeight + 2) * 5,
                position.width, EditorGUIUtility.singleLineHeight);
            var colorDetection = new Rect(position.x, position.y + (EditorGUIUtility.singleLineHeight + 2) * 6,
                position.width, EditorGUIUtility.singleLineHeight);

            EditorGUI.BeginChangeCheck();

            EditorGUI.PropertyField(angle, property.FindPropertyRelative("Angle"), new GUIContent("angle"));
            EditorGUI.PropertyField(triangleAmount, property.FindPropertyRelative("TriangleAmount"),
                new GUIContent("triangleCount"));
            EditorGUI.PropertyField(radius, property.FindPropertyRelative("Radius"), new GUIContent("radius"));
            EditorGUI.PropertyField(fragment, property.FindPropertyRelative("Fragment"), new GUIContent("fragment"));
            EditorGUI.PropertyField(colorPawn, property.FindPropertyRelative("ColorPawn"), new GUIContent("colorPawn"));
            EditorGUI.PropertyField(colorDetection, property.FindPropertyRelative("ColorDetection"),
                new GUIContent("colorDetection"));

            UpdateGraph(property, position);

            EditorGUI.EndProperty();
        }
    }
}