using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WrongAnswersDirector : MonoBehaviour
{
    public GameObject buttonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        var scrollView = GameObject.Find("Scroll View");
        var viewPort = scrollView.transform.Find("Viewport");
        Transform content = viewPort.transform.Find("Content");

        for(int i=0; i<CubeSetup.CubeDirector.wronganswers.Count; i++)
        {
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(content);
            button.GetComponentInChildren<Text>().text = $"Q. {CubeSetup.CubeDirector.wronganswers[i]}";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
