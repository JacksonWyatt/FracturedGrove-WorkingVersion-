using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DebugObj : MonoBehaviour
{
    public GameObject map;
    // Start is called before the first frame update
    void Start()
    {
        if (EditorApplication.isPlaying || Application.isPlaying)
        {
            if (map != null)
            {
                GameObject[] objects = getDescendants(GameObject.Find(""+map.name));
                foreach (GameObject obj in objects)
                {
                    if (obj != null && obj.TryGetComponent<MeshRenderer>(out MeshRenderer component))
                    {
                        MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
                        if (renderer.material.name.Contains("Debug_") || renderer.material.name.Contains("Editor_"))
                        {
                            renderer.enabled = false;
                        }
                    }
                }
            }
            
            //MeshRenderer renderer = GetComponent<MeshRenderer>();
            //renderer.enabled = false;
        }

        else return;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject[] getDescendants(GameObject obj)
    {
        //return null if no object is present
        if (obj == null) return null;
        //Get list of all  transforms in the main children object//
        Transform[] childrenT = obj.GetComponentsInChildren<Transform>();
        GameObject[] children = new GameObject[childrenT.Length - 1];

        //Add each object of the transforms to the childrens list
        for (int i = 0; i < children.Length; i++)
        {

            children[i] = childrenT[i + 1].gameObject;
            //print("" + children[i]);

        }

        return children;
    }
}
