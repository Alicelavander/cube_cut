using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(Renderer))]

public class CreateMeshDirector : MonoBehaviour
{
    private void Start()
    {
        this.GetComponent<Renderer>().material.shader = Shader.Find("Custom/standard_cullOff");
    }
}