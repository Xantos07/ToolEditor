using System.Collections.Generic;
using UnityEngine;

namespace ToolEditor
{
    public class DesignCircle : MonoBehaviour
    {
        [SerializeField] private CircleData _circleData; 
        [SerializeField] private DetectionEnvironment _detectionEnvironment;
        
        [Header("MeshFilter")]
        [SerializeField] private MeshFilter _partPos; 
        [SerializeField] private MeshFilter _partNeg;  

        private void OnValidate()
        {
            GenerateCircleMesh(false, _partPos);
            GenerateCircleMesh(true, _partNeg);
        }

        private void Update()
        {
            GenerateCircleMesh(false, _partPos);
            GenerateCircleMesh(true, _partNeg);
        }

        [ContextMenu("Generate mesh")]
        private void GenerateCircleMesh(bool positif, MeshFilter meshFilter)
        {
            Mesh mesh = new Mesh();

            //vertices
            List<Vector3> vertices = new List<Vector3> {Vector3.zero};

            /*
            vertices.AddRange(positif
                ? _detectionEnvironment.DetectElement(_circleData, true)
                : _detectionEnvironment.DetectElement(_circleData, false));*/

            vertices.AddRange(positif
                ? _circleData.HalfCirclePoints(Orientation.XZ, true)
                : _circleData.HalfCirclePoints(Orientation.XZ, false));

            //triangles 
            int[] triangles = new int[(vertices.Count- 2) * 3];
            
            // Création des triangles
            for (int i = 0; i < vertices.Count - 2; i++)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
            
            for (int i = 0; i < vertices.Count; i++)
            {
                vertices[i] *= _circleData.Radius;
            }
            
            // Mettre à jour le mesh
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles;

            if (positif) FlipTriangle(mesh);

            meshFilter.mesh = mesh;
        }

        private void FlipTriangle(Mesh mesh)
        {
            int[] trianglesFlip = mesh.triangles;

            for (int i = 0; i < trianglesFlip.Length; i += 3)
            {
                (trianglesFlip[i], trianglesFlip[i + 2]) = (trianglesFlip[i + 2], trianglesFlip[i]);
            }

            mesh.triangles = trianglesFlip;
        }

        private void OnDrawGizmos()
        {
            List<Vector3> points = _circleData.MultipleHalfCirclePoints();

            foreach (var point in points)
            {
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }
}