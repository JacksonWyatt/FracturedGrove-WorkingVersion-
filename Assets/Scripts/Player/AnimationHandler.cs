using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{

    public PlayerMovement Mover;
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Mover.state == PlayerMovement.MovementState.idle && Mover.GetGrounded() && !animator.GetBool("ReturnToIdle")) 
        {
            animator.SetBool("ReturnToIdle", true);
        }
        else if ((Mover.state == PlayerMovement.MovementState.air || !Mover.GetGrounded()) && !Mover.IsJumping() && !animator.GetBool("Falling"))
        {
            animator.SetBool("Falling", true);
        }
        else if (Mover.state == PlayerMovement.MovementState.walking && Mover.GetGrounded() && !animator.GetBool("IsWalkingForward"))
        {
            animator.SetBool("IsWalkingForward", true);
        }
        else if (Mover.state == PlayerMovement.MovementState.sprinting && Mover.GetGrounded() && !animator.GetBool("RunningForward"))
        {
            animator.SetBool("RunningForward", true);
        }
        else if ((Mover.state == PlayerMovement.MovementState.vaulting || Mover.IsJumping()) && !animator.GetBool("Jump"))
        {
            animator.SetBool("Jump", true);
        }   
    }

    private void ResetStates()
    {
        animator.SetBool("ReturnToIdle", false);
        animator.SetBool("Falling", false);
        animator.SetBool("IsWalkingForward", false);
        animator.SetBool("RunningForward", false);
        animator.SetBool("Jump", false);

    }
}