using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cube_cut;
using Cubes;

public class AnswerSceneDirector : MonoBehaviour
{
    public Text score;
    public GameObject spherePrefab;
    public Text tutorial;
    AnswerCameraDirector answerCameraDirector;
    GameObject[] sphereObjects = new GameObject[3];


    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.QuestionNumber() == 1) tutorial.color = new Color(0.7f, 0.7f, 0.7f, 1);

        List<Vector3> currentAnswers = GameManager.Instance.CurrentQuestion.answerOptions[GameManager.Instance.CurrentQuestion.answer];
        if (GameManager.Instance.CurrentQuestion.IsCorrect) score.text = "正解";
        else score.text = "不正解";

        for (int j = 0; j < 3; j++)
        {
            sphereObjects[j] = Instantiate(spherePrefab);
            sphereObjects[j].transform.position = currentAnswers[j];
        }

        answerCameraDirector = GameObject.Find("AnswerCameraDirector").GetComponent<AnswerCameraDirector>();
        answerCameraDirector.DrawMesh(answerCameraDirector.createMeshDirectorPrefab, currentAnswers);
    }

    public void OnClick()
    {
        if (GameManager.Instance.QuestionNumber() == 20) SceneManager.LoadScene("FinishRound");
        else SceneManager.LoadScene("Question");
    }
}
