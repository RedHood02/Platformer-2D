using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    [SerializeField] Animator playerAnim;
    [SerializeField] PlayerMovement playerMov;

    private void Update()
    {
        ControlAnimations();
        ControlSpeed();
    }

    void ControlSpeed()
    {
        if (Input.GetAxisRaw("Vertical") == 0 && playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Ladder"))
        {
            playerAnim.speed = 0;
            if(!playerAnim.GetBool("IsNearLadder"))
            {
                playerAnim.speed = 1;
            }
        }
        else
        {
            playerAnim.speed = 1;
        }
    }

    void ControlAnimations()
    {
        playerAnim.SetBool("IsMoving", playerMov.GetIsMoving());
        playerAnim.SetBool("IsGrounded", playerMov.GetIsGrounded());
        playerAnim.SetBool("IsNearLadder", playerMov.GetIsNearLadder());
        playerAnim.SetBool("IsClimbingLadder", playerMov.GetIsClimbing());
    }

}
