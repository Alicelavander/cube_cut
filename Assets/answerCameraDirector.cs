using CubeSetup;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class AnswerCameraDirector : MonoBehaviour
{
    public GameObject[] sphereObject;
    public GameObject createMeshDirectorPrefab;
    Vector3[] threePointsShifted;
    Vector3 p4;
    Vector3 n;
    Vector3 cp;

    public void MoveCamera(Vector3 shiftVector, List<Vector3> threePoints)
    {
        sphereObject = GameObject.Find("Cube").GetComponent<CubeDirector>().sphereObject;
        threePointsShifted = new Vector3[threePoints.Count];

        for (int i = 0; i < threePoints.Count; i++)
        {
            threePointsShifted[i] = threePoints[i] + shiftVector;
        }

        DrawMesh(shiftVector);
    }

    private void DrawMesh(Vector3 shiftVector)
    {
        GameObject createMeshDirector;
        createMeshDirector = Instantiate(createMeshDirectorPrefab) as GameObject;

        var mesh = new Mesh();
        mesh.vertices = threePointsShifted;
        var triangles = new List<int>();
        for (int i = 0; i < threePointsShifted.Length; i++)
        {
            triangles.Add(i);
        }
        mesh.SetTriangles(triangles, 0);
        var filter = createMeshDirector.GetComponent<MeshFilter>();
        filter.sharedMesh = mesh;

        LookAtMesh(createMeshDirector, shiftVector);
    }

    private void LookAtMesh(GameObject createMeshDirector, Vector3 shiftVector)
    {
        p4 = Vector3.Cross(threePointsShifted[1] - threePointsShifted[0], threePointsShifted[2] - threePointsShifted[0]);
        n = Vector3.Normalize(p4);
        cp = n * 3;

        this.transform.position = createMeshDirector.transform.position + cp + shiftVector;

        Vector3 meshPosition = createMeshDirector.transform.position + shiftVector;

        this.transform.LookAt(meshPosition);
    }
}
