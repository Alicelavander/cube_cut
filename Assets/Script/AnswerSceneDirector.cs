using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerSceneDirector : MonoBehaviour
{
    public Text score;
    // Start is called before the first frame update
    void Start()
    {
        if (CubeSetup.CubeDirector.answer == AnswerButtonScript.answerNumber) score.text = "正解";
        else score.text = "不正解";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
