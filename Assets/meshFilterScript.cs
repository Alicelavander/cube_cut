using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CubeSetup {

    public class meshFilterScript : MonoBehaviour
    {
        MeshFilter meshFilter;
        Vector3[] vertices;
        Vector3[] objectVertices;
        List<Senbun> senbunList = new List<Senbun>();
        public List<Vector3> threePoints;
        public GameObject linePrefab;
        public GameObject spherePrefab;
        public GameObject[] sphereObject = new GameObject[3];
        GameObject answerCameraDirector;

        // Start is called before the first frame update
        void Start()
        {
            answerCameraDirector = GameObject.Find("answerCamera");
            this.meshFilter = GameObject.Find("Cube").GetComponent<MeshFilter>();
            vertices = meshFilter.mesh.vertices;
            objectVertices = new Vector3[8];

            cubeVertices(vertices, objectVertices);
            //情報を処理しやすいように別のリストに入れるよ
            senbunList.Add(new Senbun(transform.TransformPoint(objectVertices[2]), transform.TransformPoint(objectVertices[4])));
            senbunList.Add(new Senbun(transform.TransformPoint(objectVertices[1]), transform.TransformPoint(objectVertices[7])));
            senbunList.Add(new Senbun(transform.TransformPoint(objectVertices[0]), transform.TransformPoint(objectVertices[6])));
            senbunList.Add(new Senbun(transform.TransformPoint(objectVertices[3]), transform.TransformPoint(objectVertices[5])));
            senbunList.Add(new Senbun(transform.TransformPoint(objectVertices[0]), transform.TransformPoint(objectVertices[2])));
            senbunList.Add(new Senbun(transform.TransformPoint(objectVertices[5]), transform.TransformPoint(objectVertices[7])));
            senbunList.Add(new Senbun(transform.TransformPoint(objectVertices[4]), transform.TransformPoint(objectVertices[6])));
            senbunList.Add(new Senbun(transform.TransformPoint(objectVertices[1]), transform.TransformPoint(objectVertices[3])));
            senbunList.Add(new Senbun(transform.TransformPoint(objectVertices[2]), transform.TransformPoint(objectVertices[3])));
            senbunList.Add(new Senbun(transform.TransformPoint(objectVertices[6]), transform.TransformPoint(objectVertices[7])));
            senbunList.Add(new Senbun(transform.TransformPoint(objectVertices[4]), transform.TransformPoint(objectVertices[5])));
            senbunList.Add(new Senbun(transform.TransformPoint(objectVertices[0]), transform.TransformPoint(objectVertices[1])));

            threePoints = decidePoints(new List<Senbun>(senbunList));


            //線を描画するよ
            GameObject lineObject;
            LineRenderer lineRenderer;
            for (int i = 0; i < senbunList.Count; i++)
            {
                lineObject = Instantiate(linePrefab) as GameObject;
                lineRenderer = lineObject.GetComponent<LineRenderer>();

                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, senbunList[i].p1);
                lineRenderer.SetPosition(1, senbunList[i].p2);
            }
            //点を描画するよ
            for (int j = 0; j < 3; j++)
            {
                sphereObject[j] = Instantiate(spherePrefab) as GameObject;
                sphereObject[j].transform.position = threePoints[j];
            }

            answerCameraDirector.GetComponent<answerCameraDirector>().moveCamera();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Mouse clicked");
                //3点をつくるよ(これ一行でできるよ)
                threePoints = decidePoints(new List<Senbun>(senbunList));
                //辺上の点の位置を動かすよ
                for (int j = 0; j < 3; j++)
                {
                    sphereObject[j].transform.position = threePoints[j];
                }
                answerCameraDirector.GetComponent<answerCameraDirector>().moveCamera();
            }
        }

        //ダブってた頂点の情報を整理してくれるよ
        void cubeVertices(Vector3[] vertices, Vector3[] objectVertices)
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
                    Debug.Log(vertices[i]);
                    objectVertices[index] = vertices[i];
                    index++;
                }
            }
        }

        //ランダム要素…３辺＆3点を決める
        List<Vector3> decidePoints(List<Senbun> list)
        {
            List<Vector3> result = new List<Vector3>();
            while (result.Count != 3)
            {
                int randomLine = Random.Range(0, list.Count -1);
                Senbun a = list[randomLine];
                int randomPoint = Random.Range(1, 9);
                Vector3 pos = Middlepoint(a, randomPoint);

                if (result.Count == 2 && pointsOnOneSide(result[0], result[1], pos) == true)
                {
                    Debug.Log("continue");
                    continue;
                }
                list.RemoveAt(randomLine);
                result.Add(pos);
            }

            return result;
        }

        //3点が同一平面上にあるかどうかを返してくれるよ
        private bool pointsOnOneSide(Vector3 a, Vector3 b, Vector3 c)
        {
            if (a.x == b.x && b.x == c.x && a.x == c.x)
            {
                return true;
            }else if (a.y == b.y && b.y == c.y && a.y == c.y)
            {
                return true;
            }
            else if (a.z == b.z && b.z == c.z && a.z == c.z)
            {
                return true;
            }
            return false;
        }

        //辺上の点の座標を返してくれるよ
        Vector3 Middlepoint(Senbun senbun, int random)
        {
            float middleX = senbun.p1.x + (senbun.p2.x - senbun.p1.x) / 10 * random;
            float middleY = senbun.p1.y + (senbun.p2.y - senbun.p1.y) / 10 * random;
            float middleZ = senbun.p1.z + (senbun.p2.z - senbun.p1.z) / 10 * random;
            Vector3 middlepoint = new Vector3(middleX, middleY, middleZ);

            return middlepoint;
        }


    }
    public class Senbun
    {
        public Vector3 p1;
        public Vector3 p2;

        public Senbun(Vector3 p1, Vector3 p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }
        
    }
}