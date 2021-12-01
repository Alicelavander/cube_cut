using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cube_cut;
using CubeSetup;

public class AnswerSceneDirector : CubeDirectorBase
{
    public Text score;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.currentQuestion.IsCorrect) score.text = "正解";
        else
        {
            score.text = "不正解";
        }

        //Cubeの描写
        DrawCubeLine();

        //答えの描写(On Cube)
    }

    public void OnClick()
    {
        if (GameManager.Instance.questionNumber() == 20) SceneManager.LoadScene("FinishRound");
        else
        {
            SceneManager.LoadScene("Question");
        }
    }
}
