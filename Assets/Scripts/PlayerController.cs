using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float speed = 200;
    public float jumpForce = 10;
    Vector3 move;
    Rigidbody2D rb;

    public LayerMask groundLayer;
    public Vector2 groundCheckerBoxSize;
    public float groundCheckerCastDistance;
    float x, y;

    public bool lastKeyStroke = false;
    public LimitMovement limitMovement;
    public bool isDrowning;

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
    }


    void MovePlayer()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        move = new Vector3(x, 0);
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
            return true;
        }
        else
        {
            return false;
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

    public bool GetIsDrowning()
    {
        return isDrowning;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position - transform.up * groundCheckerCastDistance, groundCheckerBoxSize);
    }
}