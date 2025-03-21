using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    //public float x;
    //public float y;
    //public float z;
    // Start is called before the first frame update
    GameObject[] children;
    Vector3 dest;
    GameObject teleportObj;
    GameObject teleportPlatform;
    LayerMask whatIsPlayer;
    Vector3 midpoint;
    Collision col;
    void Start()
    {
        //Vector3 dest = new Vector3(x, y, z);
        children = getDescendants(transform.gameObject);

        dest = getObjectWithName(children, "Tele_Dest").transform.position;
        teleportPlatform = getObjectWithName(children, "Tele_Platform");
        whatIsPlayer = LayerMask.GetMask("whatIsPlayer");
        midpoint = teleportPlatform.GetComponent<Renderer>().bounds.center;

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Physics.Raycast(midpoint, Vector3.up, 0.5f, whatIsPlayer))
        {
            Debug.DrawRay(midpoint, Vector3.up * 0.5f, Color.green);
            teleportObj.transform.position = dest;
        }
        else
        {
            Debug.DrawRay(midpoint, Vector3.up * 0.5f, Color.red);
        }
        */
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if(teleportPlatform.GetComponent<Collider>() == collision.GetContact(0).thisCollider)
        {
            //Debug.Log("Teleport Platform touched");
            // Add your collision handling logic here based on the child
            teleportObj = collision.gameObject;
            teleportObj.transform.position = dest;
        }
        //print("test message");
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

    public GameObject getObjectWithName(GameObject[] objects, string name)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].name == name)
            {
                return objects[i];
            }
        }
        return null;
    }
}
