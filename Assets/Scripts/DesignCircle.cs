using System;
using System.Collections.Generic;
using UnityEngine;

public class DesignCircle : MonoBehaviour
{
   [SerializeField] private CircleData _circleData;
   
   [SerializeField] private MeshFilter _meshFilterPos;
   [SerializeField] private MeshFilter _meshFilterNeg;

   private void OnValidate()
   {
      GenerateCircleMesh(false, _meshFilterNeg);
      GenerateCircleMesh(true, _meshFilterPos);
   }

   [ContextMenu("GenerateCircleMesh")]
   private void GenerateCircleMesh(bool halfCirclePositif, MeshFilter meshFilter)
   {
      Mesh mesh = new Mesh();
      
      //vertices
      List<Vector3> vertices = new List<Vector3>() {Vector3.zero};
      
      vertices.AddRange(_circleData.HalfCirclePoints(Orientation.XZ, halfCirclePositif));

      //triangles
      int[] triangles = new int[(vertices.Count - 2) * 3];
      
      //Création de nos triangles
      for (int i = 0; i < vertices.Count-2; i++)
      {
         triangles[i * 3] = 0;
         triangles[i * 3 + 1] = i + 1;
         triangles[i * 3 + 2] = i + 2;
      }

      for (int i = 0; i < vertices.Count; i++)
      {
         vertices[i] *= _circleData.Radius;
      }

      mesh.vertices = vertices.ToArray();
      mesh.triangles = triangles;

      if(halfCirclePositif) FlipTriangle(mesh);
         
      meshFilter.mesh = mesh;
   }

   private void FlipTriangle(Mesh mesh)
   {
      int[] trianglesFlip = mesh.triangles;

      for (int i = 0; i < trianglesFlip.Length; i+=3)
      {
         int triangle = trianglesFlip[i];
         trianglesFlip[i] = trianglesFlip[i + 2];
         trianglesFlip[i + 2] = triangle;
      }

      mesh.triangles = trianglesFlip;
   }
}
