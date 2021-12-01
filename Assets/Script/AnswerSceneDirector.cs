using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnswerSceneDirector : MonoBehaviour
{
    public Text score;

    // Start is called before the first frame update
    void Start()
    {
        if (CubeSetup.CubeDirector.answer == AnswerButtonScript.answerNumber) score.text = "正解";
        else
        {
            score.text = "不正解";
            CubeSetup.CubeDirector.wronganswers.Add(CubeSetup.CubeDirector.questionNumber);
        }
    }

    public void OnClick()
    {
        if (CubeSetup.CubeDirector.questionNumber == 2) SceneManager.LoadScene("FinishRound");
        else
        {
            SceneManager.LoadScene("Question");
            CubeSetup.CubeDirector.questionNumber++;
        }
    }
}
