using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed, playerJumpForce;
    [SerializeField] float x;
    [SerializeField] bool canJump;
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] LayerMask whatIsGround;


    private void Update()
    {
        GetAxis();
        PlayerMove();
    }

    private void FixedUpdate()
    {
        RaycastGround();
    }

    void GetAxis()
    {
        x = Input.GetAxisRaw("Horizontal");
    }

    void PlayerMove()
    {
        Vector2 playerAxis = new(x, 0);
        if(x != 0)
        {
            transform.Translate(playerSpeed * Time.deltaTime * playerAxis);
        }

        if(canJump && Input.GetKeyDown(KeyCode.Space))
        {
            playerRB.AddRelativeForce(transform.up * playerJumpForce, ForceMode2D.Impulse);
            canJump = false;
        }
    }

    void RaycastGround()
    {
        if(Physics2D.Raycast(transform.position, -transform.up, 1f, whatIsGround))
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, -transform.up * 1);
    }
}
