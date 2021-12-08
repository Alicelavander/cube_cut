using Cubes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class AnswerCameraDirector : MonoBehaviour
{
    public class VectorSort : IComparer
    {
        Vector3 p1;
        public VectorSort(Vector3 p1)
        {
            this.p1 = p1 + new Vector3(1, 0, 0) ;
        }
        public int Compare(object x, object y)
        {
            Vector3 px = (Vector3)x;
            Vector3 py = (Vector3)y;

            return (int)(Vector3.Dot(p1, px) - Vector3.Dot(p1, py));

            throw new System.NotImplementedException();
        }
        //p1とｘの内積、ｐ１とｙの内積を求めてリザルトで大なり小なり返す
    }
    public GameObject[] sphereObject;
    public GameObject createMeshDirectorPrefab;
    public List<Vector3> sortedList;
    List<Vector3> crossPointsShifted;
    Vector3 p4;
    Vector3 n;
    Vector3 cp;

    public void MoveCamera(Vector3 shiftVector, List<Vector3> crossPoints)
    {
        sphereObject = GameObject.Find("QuestionSceneDirector").GetComponent<QuestionSceneDirector>().sphereObject;
        crossPointsShifted = new List<Vector3>();

        //Debug.Log("crossPoints: " + crossPoints.Count);
        for (int i = 0; i < crossPoints.Count; i++)
        {
            crossPointsShifted.Add(crossPoints[i] + shiftVector);
        }

        DrawMesh(shiftVector);
    }

    private void DrawMesh(Vector3 shiftVector)
    {
        GameObject createMeshDirector;
        createMeshDirector = Instantiate(createMeshDirectorPrefab) as GameObject;

        var mesh = new Mesh();
        //crossPointsShiftedの並び替え
        sortedList = GetPointsSort(crossPointsShifted);

        //Debug.Log($"number of vertices: {sortedList.Count}");
        mesh.vertices = sortedList.ToArray();
        var triangles = new List<int>();
        for (int i = 0; i < sortedList.Count - 2; i++)
        {
            triangles.Add(0);
            triangles.Add(1 + i);
            triangles.Add(2 + i);
        }
        mesh.SetTriangles(triangles, 0);
        var filter = createMeshDirector.GetComponent<MeshFilter>();
        filter.sharedMesh = mesh;

        LookAtMesh(createMeshDirector, shiftVector, sortedList);
    }

    private void LookAtMesh(GameObject createMeshDirector, Vector3 shiftVector, List<Vector3> sortedList)
    {
        p4 = Vector3.Cross(sortedList[1] - sortedList[0], sortedList[2] - sortedList[0]);
        n = Vector3.Normalize(p4);
        cp = n * 2;

        this.transform.position = createMeshDirector.transform.position + cp + shiftVector;

        Vector3 meshPosition = createMeshDirector.transform.position + shiftVector;

        this.transform.LookAt(meshPosition);
    }

    //内積の小さい順に並べるよ
    private List<Vector3> GetPointsSort(List<Vector3> list)
    {
        //Debug.Log("Original list");
        //for (int i = 0; i < list.Count(); i++) Debug.Log(list[i]);

        Vector3 list_zero = list[0];
        Vector3 minDisVec = list[1];
        for (int k = 1; k < list.Count(); k++)
        {
            if (Vector3.Distance(list_zero, minDisVec) > Vector3.Distance(list_zero, list[k])) minDisVec = list[k];
        }
        //Debug.Log("minDisVec: " + minDisVec);

        list.Remove(list_zero);
        list.Remove(minDisVec);
        list.Sort(new VectorComparer(minDisVec - list_zero, list_zero));
        list.Insert(0, minDisVec);
        list.Insert(0, list_zero);

        //Debug.Log("Sorted list");
        //for(int i=0; i<list.Count(); i++) Debug.Log(list[i]);

        return list;
    }
}

class VectorComparer : IComparer<Vector3>
{
    Vector3 v0;
    Vector3 v1;

    public VectorComparer(Vector3 v0, Vector3 v1)
    {
        this.v0 = v0;
        this.v1 = v1;
    }

    public int Compare(Vector3 x, Vector3 y)
    {
        Vector3 v2 = x - v1;
        Vector3 v3 = y - v1;
        float a = Vector3.Dot(v2, v0);
        float b = Vector3.Dot(v3, v0);

        return b.CompareTo(a);
    }
}
