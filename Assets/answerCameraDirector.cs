using CubeSetup;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class answerCameraDirector : MonoBehaviour
{
    List<Vector3> threePoints;
    public GameObject[] sphereObject;
    Vector3 p4;
    Vector3 n;
    Vector3 cp;
    GameObject createMesh;

    public void moveCamera()
    {
        threePoints = GameObject.Find("Cube").GetComponent<meshFilterScript>().threePoints;
        sphereObject = GameObject.Find("Cube").GetComponent<meshFilterScript>().sphereObject;
        createMesh = GameObject.Find("CreateMeshDirector").GetComponent<createMesh>().drawMesh(threePoints);


        p4 = Vector3.Cross(threePoints[1]- threePoints[0], threePoints[2] - threePoints[0]);
        n = Vector3.Normalize(p4);
        cp = n * 5;

        this.transform.position = threePoints[0] + cp;
        this.transform.LookAt(sphereObject[0].transform, threePoints[2] - threePoints[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
