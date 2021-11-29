using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnswerButtonScript : MonoBehaviour
{
    public static int answerNumber;
    AnswerCameraDirector[] answerCameraDirector;

    // Start is called before the first frame update
    void Start()
    {
        answerCameraDirector = new AnswerCameraDirector[4];
        answerCameraDirector[0] = GameObject.Find("AnswerCamera1").GetComponent<AnswerCameraDirector>();
        answerCameraDirector[1] = GameObject.Find("AnswerCamera2").GetComponent<AnswerCameraDirector>();
        answerCameraDirector[2] = GameObject.Find("AnswerCamera3").GetComponent<AnswerCameraDirector>();
        answerCameraDirector[3] = GameObject.Find("AnswerCamera4").GetComponent<AnswerCameraDirector>();
    }

    public void OnClick(int selectedButton)
    {
        answerNumber = selectedButton;
        Debug.Log("Number " + answerNumber + " was pressed.");

        List<Vector3> sortedList = answerCameraDirector[answerNumber].sortedList;
        string coordinates = "Point({";
        for(int i=0; i<sortedList.Count; i++)
        {
            coordinates += $" {{ {sortedList[i].x}, {sortedList[i].y}, {sortedList[i].z} }}";
            if (i != sortedList.Count - 1) coordinates += ",";
        }
        coordinates += "})";
        Debug.Log(coordinates);

        SceneManager.LoadScene("Answer");
    }
}
