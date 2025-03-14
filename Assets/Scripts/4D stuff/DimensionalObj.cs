using System.Collections;
using System.Collections.Generic;
using UnityEditor;
//using UnityEditor.VersionControl;
using UnityEngine;

public class DimensionalObj : MonoBehaviour
{

    GameObject[] objects;
    public DimensionNavigation dimNav;
    string tag4D = "4D";
    string tagBoth = "Both";
    string tag3D = "3D";

    public Material activePTWall;
    public Material inactivePTWall;

    public LayerMask whatIsWall;
    public LayerMask whatIsPassThroughWall;
    public LayerMask whatIsWallAndPT;

    public LayerMask whatIsGround;
    public LayerMask whatIsPassThroughGround;
    public LayerMask whatIsGroundAndPT;
    

    // Start is called before the first frame update
    void Start()
    {
        objects = getDescendants(transform.gameObject);
        checkAndSwitchMode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
        //print("In 4D mode? " + dimNav.IsIn4D());
        checkAndSwitchMode();


    }

    public void checkAndSwitchMode()
    {  

        foreach (GameObject obj in objects)
        {
            if (!isEmpty(obj))
            {
                if (obj.tag.Equals(tag4D))
                {
                    if (dimNav.IsIn4D())
                    {
                        if (!obj.activeInHierarchy)
                            obj.SetActive(true);
                    }
                    else
                    {
                        if (obj.activeInHierarchy)
                            obj.SetActive(false);
                    }
                }

                else if (obj.tag.Equals(tag3D))
                {
                    if (dimNav.IsIn4D())
                    {
                        if (obj.layer == LayerMask.NameToLayer("whatIsWallAndPT"))
                        {
                            obj.layer = LayerMask.NameToLayer("whatIsPassthroughWall");
                            obj.GetComponent<MeshRenderer>().material = inactivePTWall;
                        }

                        else if (obj.layer == LayerMask.NameToLayer("whatIsGroundAndPT"))
                        {
                            obj.layer = LayerMask.NameToLayer("whatIsPassthroughGround");
                            obj.GetComponent<MeshRenderer>().material = inactivePTWall;
                        }

                        // Setting 3D only objects inactive when in 4D mode
                        else if (obj.layer == LayerMask.NameToLayer("whatIsIn3D"))
                            if (obj.activeInHierarchy)
                                obj.SetActive(false);

                    }
                    else
                    {
                        if (obj.layer == LayerMask.NameToLayer("whatIsPassthroughWall"))
                        {
                            obj.layer = LayerMask.NameToLayer("whatIsWallAndPT");
                            obj.GetComponent<MeshRenderer>().material = activePTWall;
                        }

                        else if (obj.layer == LayerMask.NameToLayer("whatIsPassthroughGround"))
                        {
                            obj.layer = LayerMask.NameToLayer("whatIsGroundAndPT");
                            obj.GetComponent<MeshRenderer>().material = activePTWall;
                        }

                        // Setting 3D only objects active when not in 4D mode
                        else if (obj.layer == LayerMask.NameToLayer("whatIsIn3D"))    
                            if (!obj.activeInHierarchy)
                                obj.SetActive(true);


                    }
                }

                else if (obj.tag.Equals(tagBoth))
                {
                    //do nothing
                }
            }
            
        }
    }

    public GameObject[] getDescendants(GameObject obj)
    {
        //return null if no object is present
        if (obj == null) return null;
        //Get list of all  transforms in the main children object//
        Transform[] childrenT = obj.GetComponentsInChildren<Transform>();
        GameObject[] children = new GameObject[childrenT.Length-1];

        //Add each object of the transforms to the childrens list
        for (int i = 0; i < children.Length; i++)
        {
            
                children[i] = childrenT[i+1].gameObject;
                //print("" + children[i]);
            
        }

        return children;
    }

    public bool isEmpty(GameObject obj)
    {
        Component[] allComponents = obj.GetComponents<Component>();

        if (allComponents.Length == 1) // Contains only Transform?
            return true;
        return false;
    }

}
