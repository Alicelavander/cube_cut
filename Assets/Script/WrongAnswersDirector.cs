using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cube_cut;

public class WrongAnswersDirector : MonoBehaviour
{
    public GameObject buttonPrefab;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
