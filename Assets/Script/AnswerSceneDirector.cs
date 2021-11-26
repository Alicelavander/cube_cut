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
        else score.text = "不正解";
    }

    public void OnClick()
    {
        Debug.Log("Button pressed.");
        SceneManager.LoadScene("Question");
    }
}
