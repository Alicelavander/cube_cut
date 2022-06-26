using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cube_cut;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class ReviewButtonScript : MonoBehaviour
{
    public int QuestionNumber;

    public void OnClick() {
        GameManager.Instance.SetCurrentQuestionNumber(QuestionNumber);
        SceneManager.LoadScene("Review");

        Debug.Log(QuestionNumber);
    }
}
