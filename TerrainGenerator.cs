using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private void Start() {
        #if UNITY_EDITOR    
            UnityEditor.SceneView.FocusWindowIfItsOpen(typeof(UnityEditor.SceneView));
        #endif
        mesh = new Mesh();
        CreateMesh();
        UpdateMesh();
    }
    private void CreateMesh(){
        vertices = new Vector3[] {
            new Vector3(0,0,0),
            new Vector3(0,0,100),
            new Vector3(100,0,0),
            new Vector3(100,0,100),
        };
        triangles = new int[] {
            3,0,1,
            0,3,2,
        };
    }
    private void UpdateMesh(){
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}
