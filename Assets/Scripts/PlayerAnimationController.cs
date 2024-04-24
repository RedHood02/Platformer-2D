using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] PlayerController _playerController;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
    }


    private void Update()
    {
        ControlAnims();
        ControlLadder();
    }

    void ControlAnims()
    {
        anim.SetBool("IsGrounded", _playerController.IsGrounded());
        anim.SetBool("IsMoving", _playerController.IsMoving());
        anim.SetBool("IsDrowning", _playerController.GetIsDrowning());
        anim.SetBool("InLadder", _playerController.isLadder);
    }


    void ControlLadder()
    {
        if (anim.GetBool("InLadder") && !_playerController.isClimbing)
        {
            anim.speed = 0;
        }
        else if(anim.GetBool("InLadder") && _playerController.isClimbing)
        {
            anim.speed = 1;
        }

        else
        {
            anim.speed = 1;
        }
    }

    public void ResetBool()
    {
        anim.SetBool("IsGrounded", true);
        anim.SetBool("IsMoving", false);
        anim.SetBool("IsDrowning", false);
        anim.SetBool("InLadder", false);
    }
}
