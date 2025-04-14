using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;
    public Transform PlayerModel;

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPosition.position;
        cameraPosition.localRotation = transform.localRotation;
    }
}
