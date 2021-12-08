using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cube_cut;
using UnityEngine.SceneManagement;
using Cubes;

public class WrongAnswersDirector : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        var scrollView = GameObject.Find("Scroll View");
        var viewPort = scrollView.transform.Find("Viewport");
        Transform content = viewPort.transform.Find("Content");
        List<int> wrongAnswers = GameManager.Instance.WrongAnswers();

        for(int i=0; i< wrongAnswers.Count; i++)
        {
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(content);
            button.GetComponentInChildren<Text>().text = $"Q. {wrongAnswers[i]}";
        }

        text.text = $"スコア: {20 - wrongAnswers.Count} / 20";        
    }

    // Update is called once per frame
    public void OnClick()
    {
        SceneManager.LoadScene("Start");
    }
}
