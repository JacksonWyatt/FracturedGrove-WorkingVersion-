using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementTrailHandler : MonoBehaviour
{

    public TrailRenderer LeftTrail;
    public TrailRenderer RightTrail;

    public PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.GetMoveSpeed() > playerMovement.GetMaxSpeed() / 2)
        {
            LeftTrail.emitting = true;
            RightTrail.emitting = true;
        }
        else
        {
            LeftTrail.emitting = false;
            RightTrail.emitting = false;
        }
    }
}
