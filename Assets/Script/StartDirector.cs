﻿using Cube_cut;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartDirector : MonoBehaviour
{
    public void Start()
    {
    }

    public void OnClick()
    {
        SceneManager.LoadScene("Question");
        GameManager.Instance.Reset();
    }
}
