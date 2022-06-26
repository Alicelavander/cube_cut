using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cube_cut;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReviewSceneDirector : MonoBehaviour
{
    public GameObject spherePrefab;
    public Text question;
    public Sprite right;
    public Sprite wrong;
    int currentQuestionNumber;
    AnswerCameraDirector mainCameraDirector;
    GameObject[] sphereObjects = new GameObject[3];
    public AnswerCameraDirector[] answerCameraDirectors;

    // Start is called before the first frame update
    void Start()
    {
        currentQuestionNumber = GameManager.Instance.GetCurrentQuestionNumber();
        Debug.Log(currentQuestionNumber);

        question.text = $"{currentQuestionNumber + 1}.";
        QuestionData questionData = GameManager.Instance.getQuestionData(currentQuestionNumber);

        //立方体&Mesh作成
        mainCameraDirector = GameObject.Find("AnswerCameraDirector").GetComponent<AnswerCameraDirector>();
        for (int j = 0; j < 3; j++)
        {
            sphereObjects[j] = Instantiate(spherePrefab);
            sphereObjects[j].transform.position = questionData.answerOptions[questionData.answer][j];
        }
        mainCameraDirector.DrawMesh(mainCameraDirector.createMeshDirectorPrefab, questionData.answerOptions[questionData.answer]);

        //答えの4パネル作成
        Debug.Log(answerCameraDirectors.Length);
        answerCameraDirectors = new AnswerCameraDirector[4];
        answerCameraDirectors[0] = GameObject.Find("AnswerCamera1").GetComponent<AnswerCameraDirector>();
        answerCameraDirectors[1] = GameObject.Find("AnswerCamera2").GetComponent<AnswerCameraDirector>();
        answerCameraDirectors[2] = GameObject.Find("AnswerCamera3").GetComponent<AnswerCameraDirector>();
        answerCameraDirectors[3] = GameObject.Find("AnswerCamera4").GetComponent<AnswerCameraDirector>();

        for (int i = 0; i < 4; i++)
        {
            answerCameraDirectors[i].MoveCamera(new Vector3(10 * (i + 1), 0, 0), questionData.answerOptions[i]);
        }

        //正解のパネルを緑で縁取り、選択したパネルが間違えの場合は選択したパネルを赤で表示
        if (!questionData.IsCorrect)
        {
            GameObject.Find($"AnswerButton{questionData.selectedAnswer + 1}").GetComponent<Image>().sprite = wrong;
            GameObject.Find($"AnswerButton{questionData.selectedAnswer + 1}").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        GameObject.Find($"AnswerButton{questionData.answer + 1}").GetComponent<Image>().sprite = right;
        GameObject.Find($"AnswerButton{questionData.answer + 1}").GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }

    public void OnBackClick()
    {
        SceneManager.LoadScene("FinishRound");
    }

    public void OnRotateClick()
    {
        SceneManager.LoadScene("Rotate");
    }
}
