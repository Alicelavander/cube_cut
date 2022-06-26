using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cube_cut;
using UnityEngine.SceneManagement;
using Cubes;
using System;

public class WrongAnswersDirector : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Text text;
    public Sprite right;
    public Sprite wrong; 

    bool correct(int questionNumber, List<int> wrongAnswers)
    {
        for (int i = 0; i < wrongAnswers.Count; i++)
        {
            if (questionNumber.Equals(wrongAnswers[i]))
            {
                return false;
            }
        }
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        var scrollView = GameObject.Find("Scroll View");
        var viewPort = scrollView.transform.Find("Viewport");
        Transform content = viewPort.transform.Find("Content");
        int questionNumber = GameManager.Instance.QuestionNumber();
        List<int> wrongAnswers = GameManager.Instance.WrongAnswers();
        Image image;

        for (int i=0; i< questionNumber; i++)
        {
            GameObject button = Instantiate(buttonPrefab);
            image = button.GetComponent<Image>();
            button.transform.SetParent(content);
            button.GetComponentInChildren<Text>().text = $"問{i+1}";
            button.GetComponent<ReviewButtonScript>().QuestionNumber = i;

            if (correct(i, wrongAnswers)) image.sprite = right;
            else image.sprite = wrong;
            button.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        text.text = $"スコア: {20 - wrongAnswers.Count} / 20";        
    }
    // Update is called once per frame
    public void OnClick()
    {
        SceneManager.LoadScene("Start");
    }
}
