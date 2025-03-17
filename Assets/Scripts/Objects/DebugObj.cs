using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DebugObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (EditorApplication.isPlaying || Application.isPlaying)
        {
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            renderer.enabled = false;
        }

        else return;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
