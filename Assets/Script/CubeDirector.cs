using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CubeSetup
{
    public class CubeDirector : MonoBehaviour
    {
        List<Senbun> CubeSidesList = new List<Senbun>();
        public GameObject linePrefab;
        public GameObject spherePrefab;
        public GameObject[] sphereObject = new GameObject[3];
        public AnswerCameraDirector[] answerCameraDirector;
        public Text question;
        public static List<int> wronganswers = new List<int>();
        public static int answer;
        public static int questionNumber = 1;

        // Start is called before the first frame update
        void Start()
        {
            question.text = $"{questionNumber}.";

            answerCameraDirector = new AnswerCameraDirector[4];
            answerCameraDirector[0] = GameObject.Find("AnswerCamera1").GetComponent<AnswerCameraDirector>();
            answerCameraDirector[1] = GameObject.Find("AnswerCamera2").GetComponent<AnswerCameraDirector>();
            answerCameraDirector[2] = GameObject.Find("AnswerCamera3").GetComponent<AnswerCameraDirector>();
            answerCameraDirector[3] = GameObject.Find("AnswerCamera4").GetComponent<AnswerCameraDirector>();

            //verticsをとってきて、ダブル頂点が無いように整理するよ
            MeshFilter meshFilter = GameObject.Find("Cube").GetComponent<MeshFilter>();
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

            //図形4つ決めるよ(正確にはその頂点達の座標を決めるよ)
            List<List<Vector3>> vertexList = new List<List<Vector3>>();
            List<int> numberOfVertices = new List<int> { 3, 4, 5, 6 };
            while (numberOfVertices.Count != 0)
            {
                List<Vector3> Points = DecidePoints(new List<Senbun>(CubeSidesList));
                if (numberOfVertices.Find(n => n == Points.Count) != 0)
                {
                    vertexList.Add(Points);
                    numberOfVertices.Remove(Points.Count);
                }
            }

            //立方体を描画するよ
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
            //答えになる図形を決めるよ
            answer = Random.Range(0, 4);
            //立方体上に3点を描画するよ
            for (int j = 0; j < 3; j++)
            {
                sphereObject[j] = Instantiate(spherePrefab);
                sphereObject[j].transform.position = vertexList[answer][j];
            }
            Debug.Log($"answer is: {answer}");
            //カメラを移動させるよ
            for (int i = 0; i < 4; i++)
            {
                answerCameraDirector[i].MoveCamera(new Vector3(10 * (i + 1), 0, 0), vertexList[i]);
            }
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

        List<Vector3> DecidePoints(List<Senbun> list)
        {
            List<Vector3> threePoints = new List<Vector3>();
            List<Senbun> listClone = new List<Senbun>(list);

            //ランダムで3点を決める
            while (threePoints.Count != 3)
            {
                int randomLine = Random.Range(0, listClone.Count);
                Senbun a = listClone[randomLine];
                int randomPoint = Random.Range(1, 4);
                Vector3 pos = PointOnLine(a, randomPoint);

                if (threePoints.Count == 2 && PointsOnOneSide(threePoints[0], threePoints[1], pos) == true)
                {
                    continue;
                }
                listClone.RemoveAt(randomLine);
                threePoints.Add(pos);
            }
            //Debug.Log($"{threePoints.Count} threePoints: {threePoints[0]}, {threePoints[1]},{threePoints[2]}");

            //上の3点を通った平面で切断したときに通る他の点もリストに追加する
            Plane plane = new Plane(threePoints[0], threePoints[1], threePoints[2]);
            List<Vector3> result = new List<Vector3>();
            for (int i = 0; i < list.Count; i++)
            {
                Senbun senbun = list[i];
                Vector3 v1 = senbun.p1;
                Vector3 v2 = senbun.p2;

                //平面上の点P
                Vector3 P = new Vector3(plane.normal.x * plane.distance, plane.normal.y * plane.distance, plane.normal.z * plane.distance);

                //PA PBベクトル
                Vector3 PA = new Vector3(P.x - v1.x, P.y - v1.y, P.z - v1.z);
                Vector3 PB = new Vector3(P.x - v2.x, P.y - v2.y, P.z - v2.z);

                double dot_PA = PA.x * plane.normal.x + PA.y * plane.normal.y + PA.z * plane.normal.z;
                double dot_PB = PB.x * plane.normal.x + PB.y * plane.normal.y + PB.z * plane.normal.z;

                if (System.Math.Abs(dot_PA) < 0.000001) { dot_PA = 0.0; }
                if (System.Math.Abs(dot_PB) < 0.000001) { dot_PB = 0.0; }

                if (dot_PA == 0.0 && dot_PB == 0.0) continue;
                else if ((dot_PA >= 0.0 && dot_PB <= 0.0) || (dot_PA <= 0.0 && dot_PB >= 0.0))
                {
                    Vector3 AB = new Vector3(v2.x - v1.x, v2.y - v1.y, v2.z - v1.z);

                    //交点とAの距離 : 交点とBの距離 = dot_PA : dot_PB
                    double ratio = System.Math.Abs(dot_PA) / (System.Math.Abs(dot_PA) + System.Math.Abs(dot_PB));


                    Vector3 crossPoint = new Vector3();
                    crossPoint.x = (float)(v1.x + (AB.x * ratio));
                    crossPoint.y = (float)(v1.y + (AB.y * ratio));
                    crossPoint.z = (float)(v1.z + (AB.z * ratio));

                    //Debug.Log($"Adding new crosspoint: {crossPoint}, ratio: {ratio}, dot_PA: {dot_PA}, dot_PB: {dot_PB}");
                    bool overlap = false;
                    for (int j = 0; j < result.Count; j++)
                    {
                        if (result[j] == crossPoint) overlap = true;
                    }
                    if (!overlap) result.Add(crossPoint);
                }
            }
            return result;
        }

        //3点が同一平面上にあるかどうかを返してくれるよ
        private bool PointsOnOneSide(Vector3 a, Vector3 b, Vector3 c)
        {
            if (a.x == b.x && b.x == c.x && a.x == c.x)
            {
                return true;
            }
            else if (a.y == b.y && b.y == c.y && a.y == c.y)
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
        Vector3 PointOnLine(Senbun senbun, int random)
        {
            float middleX = senbun.p1.x + (senbun.p2.x - senbun.p1.x) / 4 * random;
            float middleY = senbun.p1.y + (senbun.p2.y - senbun.p1.y) / 4 * random;
            float middleZ = senbun.p1.z + (senbun.p2.z - senbun.p1.z) / 4 * random;
            Vector3 pointCoordinate = new Vector3(middleX, middleY, middleZ);

            return pointCoordinate;
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