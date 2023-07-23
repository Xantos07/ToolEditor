using System.Collections.Generic;
using UnityEngine;
public class DesignCircle : MonoBehaviour
{
    [SerializeField] private CircleData _circleData;
    
    [SerializeField] private MeshFilter _partPos;
    [SerializeField] private MeshFilter _partNeg;
    private int[] triangles;

    private void OnValidate()
    {
        GenerateCircleMesh(false);
        GenerateCircleMesh(true);
    }

    [ContextMenu("Generate mesh")]
    void GenerateCircleMesh(bool positif)
    {
        Mesh mesh = new Mesh();
        
        //vertices
        List<Vector3> vertices = new List<Vector3>();
        
        vertices.Add(Vector3.zero);
        vertices.AddRange(_circleData.CirclesPoints(false));
        
        if(!positif)
        {
            vertices = new List<Vector3>();
            
            vertices.Add(Vector3.zero);
            vertices.AddRange(_circleData.CirclesPoints(false));
            
            for (int i = 0; i < vertices.Count; i++)
                vertices[i] = new Vector3(vertices[i].x, 0, -vertices[i].z);
        }

        //triangles 
        triangles = new int[(vertices.Count-1) * 3];

        // Création des triangle
        for (int i = 0; i < vertices.Count -1; i++)
        {
            triangles[0] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }
        
        triangles[^1] = 0;

        for (int i = 0; i < vertices.Count; i++)
        {
            vertices[i] = vertices[i] * _circleData._radius;
        }
        
        // Mettre à jour le mesh
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles;

        if(positif)
            FlipNormal(mesh);

        if(positif)
            _partPos.mesh = mesh;
        else
            _partNeg.mesh = mesh;
    }

    void FlipNormal(Mesh mesh)
    {
        Vector3[] normals = mesh.normals;
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = -normals[i];
        }
        mesh.normals = normals;

        int[] trianglesFlip = mesh.triangles;
        
        for (int i = 0; i < trianglesFlip.Length; i += 3)
        {
            (trianglesFlip[i], trianglesFlip[i + 2]) = (trianglesFlip[i + 2], trianglesFlip[i]);
        }
        
        mesh.triangles = trianglesFlip;

        mesh.RecalculateBounds();
    }
}
