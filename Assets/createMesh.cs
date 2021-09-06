using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class createMesh : MonoBehaviour
{
    public void drawMesh(List<Vector3> Vector3List)
    {
        var mesh = new Mesh();
        mesh.vertices = Vector3List.ToArray();
        mesh.RecalculateNormals();
        var filter = GetComponent<MeshFilter>();
        filter.sharedMesh = mesh;
    }
}