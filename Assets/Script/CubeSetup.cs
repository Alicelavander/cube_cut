using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cubes;

public class CubeSetup : MonoBehaviour
{
    public GameObject linePrefab;
    public List<Senbun> CubeSidesList = new List<Senbun>();

    // Start is called before the first frame update
    void Start()
    {
        SetCubeLine(this.gameObject);
        DrawCubeLine(linePrefab);
    }

    //use linePrefab
    public void DrawCubeLine(GameObject linePrefab)
    {
        GameObject lineObject;
        LineRenderer lineRenderer;
        for (int i = 0; i < CubeSidesList.Count; i++)
        {
            lineObject = Instantiate(linePrefab);
            lineRenderer = lineObject.GetComponent<LineRenderer>();

            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, CubeSidesList[i].p1);
            lineRenderer.SetPosition(1, CubeSidesList[i].p2);
        }
    }

    //use cube
    public void SetCubeLine(GameObject cube)
    {
        //verticsをとってきて、ダブル頂点が無いように整理するよ
        MeshFilter meshFilter = cube.GetComponent<MeshFilter>();
        Vector3[] objectVertices = new Vector3[8];
        SimplifyVertices(meshFilter.mesh.vertices, objectVertices);

        //情報を処理しやすいように別のリストに入れるよ
        CubeSidesList.Add(new Senbun(transform.TransformPoint(objectVertices[2]), transform.TransformPoint(objectVertices[4])));
        CubeSidesList.Add(new Senbun(transform.TransformPoint(objectVertices[1]), transform.TransformPoint(objectVertices[7])));
        CubeSidesList.Add(new Senbun(transform.TransformPoint(objectVertices[0]), transform.TransformPoint(objectVertices[6])));
        CubeSidesList.Add(new Senbun(transform.TransformPoint(objectVertices[3]), transform.TransformPoint(objectVertices[5])));
        CubeSidesList.Add(new Senbun(transform.TransformPoint(objectVertices[0]), transform.TransformPoint(objectVertices[2])));
        CubeSidesList.Add(new Senbun(transform.TransformPoint(objectVertices[5]), transform.TransformPoint(objectVertices[7])));
        CubeSidesList.Add(new Senbun(transform.TransformPoint(objectVertices[4]), transform.TransformPoint(objectVertices[6])));
        CubeSidesList.Add(new Senbun(transform.TransformPoint(objectVertices[1]), transform.TransformPoint(objectVertices[3])));
        CubeSidesList.Add(new Senbun(transform.TransformPoint(objectVertices[2]), transform.TransformPoint(objectVertices[3])));
        CubeSidesList.Add(new Senbun(transform.TransformPoint(objectVertices[6]), transform.TransformPoint(objectVertices[7])));
        CubeSidesList.Add(new Senbun(transform.TransformPoint(objectVertices[4]), transform.TransformPoint(objectVertices[5])));
        CubeSidesList.Add(new Senbun(transform.TransformPoint(objectVertices[0]), transform.TransformPoint(objectVertices[1])));
    }

    //ダブってた頂点の情報を整理してくれるよ
    void SimplifyVertices(Vector3[] vertices, Vector3[] objectVertices)
    {
        int index = 0;
        for (int i = 0; i < vertices.Length; i++)
        {
            bool overlap = false;
            for (int j = 0; j < index; j++)
            {
                if (objectVertices[j].Equals(vertices[i]))
                {
                    overlap = true;
                    break;
                }
            }
            if (overlap == false)
            {
                objectVertices[index] = vertices[i];
                index++;
            }
        }

    }
}
