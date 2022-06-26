using Cube_cut;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RotateSceneDirector : MonoBehaviour
{
    int questionNumber = GameManager.Instance.GetCurrentQuestionNumber();
    public GameObject spherePrefab;
    GameObject[] sphereObjects = new GameObject[3];

    void Start()
    {
        List<Vector3> spherePositions = GameManager.Instance.getQuestionData(questionNumber).answerOptions[GameManager.Instance.getQuestionData(questionNumber).answer];
        for (int j = 0; j < 3; j++)
        {
            sphereObjects[j] = Instantiate(spherePrefab);
            sphereObjects[j].transform.position = spherePositions[j];
        }
        AnswerCameraDirector answerCameraDirector = GameObject.Find("AnswerCameraDirector").GetComponent<AnswerCameraDirector>();
        answerCameraDirector.DrawMesh(answerCameraDirector.createMeshDirectorPrefab, spherePositions);
    }

    // Update is called once per frame
    public void OnClick()
    {
        SceneManager.LoadScene("Review");
    }
}
