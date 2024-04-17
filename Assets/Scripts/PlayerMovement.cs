using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed, playerJumpForce;
    float x, y;
    [SerializeField] bool isGrounded, isMoving, nearLadder, isClimbingLadder, canPlayerMove;
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] BoxCollider2D playerBoxCollider;

    [SerializeField] LayerMask whatIsGround, whatIsWater;

    [SerializeField] SpriteRenderer playerSprite;

    [SerializeField] PlayerAnimController animController;
    [SerializeField] LimitMovement limitMovement;

    [SerializeField] PlayerKey playerKey;

    Vector3 playerSpawn;

    private void Awake()
    {
        playerKey = GetComponent<PlayerKey>();
        playerBoxCollider = GetComponent<BoxCollider2D>();
        animController = GetComponent<PlayerAnimController>();
        limitMovement = FindObjectOfType<LimitMovement>();
    }


    private void OnEnable()
    {
        playerSpawn = transform.position;
    }

    private void Update()
    {
        if (canPlayerMove)
        {
            GetAxis();
            PlayerMove();
            PlayerJump();
            PlayerClimb();
            ControlFlip();
            DeactivateRB();
        }
    }

    private void FixedUpdate()
    {
        RaycastGround();
    }

    void GetAxis()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
    }

    void PlayerMove()
    {
        Vector2 playerAxis = new(x, 0);
        if (x != 0)
        {
            transform.Translate(playerSpeed * Time.deltaTime * playerAxis);
            isMoving = true;

        }
        else
        {
            isMoving = false;
        }

    }

    void PlayerJump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space) && limitMovement.JumpsLeft() > 0)
        {
            limitMovement.ReduceJumps();
            playerRB.AddRelativeForce(transform.up * playerJumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void PlayerClimb()
    {
        Vector2 playerAxis = new(0, y);

        if (nearLadder && playerAxis.y != 0)
        {
            transform.Translate(playerSpeed * Time.deltaTime * playerAxis);
            isClimbingLadder = true;
        }
        else
        {
            isClimbingLadder = false;
        }
    }

    void ControlFlip()
    {
        if (x == 1)
        {
            playerSprite.flipX = false;
        }
        else if (x == -1)
        {
            playerSprite.flipX = true;
        }
    }

    void DeactivateRB()
    {
        if (nearLadder)
        {
            playerRB.isKinematic = true;
        }
        else
        {
            playerRB.isKinematic = false;
        }
    }

    void RaycastGround()
    {
        if (Physics2D.Raycast(transform.position, -transform.up, 1f, whatIsGround))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (Physics2D.Raycast(transform.position, -transform.up, 0.51f, whatIsWater))
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator PlayerDeath()
    {
        Debug.Log("Running");
        //play death animation
        canPlayerMove = false;
        yield return new WaitForSeconds(1.25f);
        if (GetComponent<PlayerWin>().GetIsKeyLevel() == true)
        {
            playerKey.ResetKey();
        }
        //Replace death scene for relocating player
        transform.position = playerSpawn;
        animController.ResetAnimation();
        limitMovement.ResetNumbers();
        ResetBools();
        ResetFlip();
        yield break;
    }

    public IEnumerator DrowningDeath()
    {
        Debug.Log("Running");
        canPlayerMove = false;
        animController.IsDrowing(true);
        playerBoxCollider.enabled = false;
        if (FindObjectOfType<PlayerWin>().GetIsKeyLevel() == true)
        {
            playerKey.ResetKey();
        }
        yield return new WaitForSeconds(1.5f);
        playerBoxCollider.enabled = true;
        transform.position = playerSpawn;
        limitMovement.ResetNumbers();
        animController.ResetAnimation();
        ResetBools();
        ResetFlip();
        yield break;

    }

    void ResetFlip()
    {
        playerSprite.flipX = false;
    }

    void ResetBools()
    {
        animController.IsDrowing(false);
        isGrounded = true;
        isMoving = false;
        nearLadder = false;
        isClimbingLadder = false;
        canPlayerMove = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, -transform.up * 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            nearLadder = true;
            playerRB.velocity = Vector2.zero;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            nearLadder = false;
        }
    }
    public bool GetIsMoving()
    {
        return isMoving;
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    public bool GetIsClimbing()
    {
        return isClimbingLadder;
    }

    public bool GetIsNearLadder()
    {
        return nearLadder;
    }

    public void SetCanPlayerMove(bool newBool)
    {
        canPlayerMove = newBool;
    }

    public bool GetCanPlayerMove()
    {
        return canPlayerMove;
    }
}