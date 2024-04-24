using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float speed = 200, climbSpeed;
    public float jumpForce = 10;
    Vector3 move;
    Rigidbody2D rb;

    public LayerMask groundLayer, ladderExit;
    public Vector2 groundCheckerBoxSize;
    public float groundCheckerCastDistance;
    float x, y;

    public bool lastKeyStroke = false;
    public LimitMovement limitMovement;
    public bool isDrowning;

    public bool HasKey;

    public bool isLadder;
    public bool isClimbing;

    // Start is called before the first frame update
    void Start()
    {
        limitMovement = GetComponent<LimitMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Jump();
        Death();

        if (isLadder && Mathf.Abs(PlayerMovementVector().y) > 0f)
        {
            isClimbing = true;
        }
    }

    void Death()
    {
        if(limitMovement.currentKeyStrokesLeft == 0 && limitMovement.currentJumpsLeft == 0)
        {
            GameManager._instance.PlayerDeath();
        }
    }

    void MovePlayer()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        if (!isLadder)
        {
            move = new Vector3(x, 0);
        }
        else
        {
            move = new Vector3(x, y);
        }
        if (!lastKeyStroke)
        {
            transform.position += speed * Time.deltaTime * move;
        }
    }

    public Vector2 PlayerMovementVector()
    {
        return new Vector2(x, y);
    }

    public bool IsMoving()
    {
        return x != 0 && !lastKeyStroke;
    }

    void Jump()
    {
        if (!limitMovement.CanJump()) return;

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, groundCheckerBoxSize, 0, -transform.up, groundCheckerCastDistance, groundLayer))
        {
            Debug.Log("Grounded");
            return true;
        }
        else
        {
            return false;
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, move.y * climbSpeed);
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Water"))
        {
            isDrowning = true;
            StartCoroutine(PlayerDrownDeath());
        }
    }

    IEnumerator PlayerDrownDeath()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(1.25f);
        GameManager._instance.PlayerDeath();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }

        if (collision.CompareTag("Key"))
        {
            GameManager._instance.SetPlayerHasKey(true);
            collision.GetComponent<Key>().isPickedUp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }


    public bool GetIsDrowning()
    {
        return isDrowning;
    }

    public void ResetDrowning()
    {
        isDrowning = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position - transform.up * groundCheckerCastDistance, groundCheckerBoxSize);
    }
}