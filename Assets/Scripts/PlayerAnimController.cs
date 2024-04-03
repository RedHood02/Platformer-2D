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
    }

    void ControlSpeed()
    {
        if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Ladder") && Input.GetAxisRaw("Horizontal") != 1)
        {
            playerAnim.speed = 0;
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
    }

}
