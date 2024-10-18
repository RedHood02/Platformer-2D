using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private SpriteRenderer _sr;
    private Rigidbody2D _rb;

    [Header("Movement")]
    [SerializeField] private float _walkSpeed = 200;
    public bool m_isMoving = false;
    private Vector3 _move;

    [Header("Movement Counter")]
    public float m_remainingJumps;
    public float RemainingJumps => m_remainingJumps;
    public float m_maxJumps;
    public float m_remainingMoves;
    public float m_maxMoves;

    [Header("Jump")]
    [SerializeField] private float _jumpForce = 10;

    [Header("Climb")]
    [SerializeField] private float _climbSpeed;
    [SerializeField] private bool _isLadder;
    [SerializeField] private bool _isClimbing;

    [Header("Keybinds")]
    private PlayerInput _playerInput;
    private Vector2 _input;

    [Header("Ground Check")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _ladderExit;
    [SerializeField] private Vector2 _groundCheckerBoxSize;
    [SerializeField] private float _groundCheckerCastDistance;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerInput = GameObject.FindGameObjectWithTag("InputManager").GetComponent<PlayerInput>();

        m_remainingJumps = m_maxJumps;
        m_remainingMoves = m_maxMoves;
    }

    private void Update()
    {
        if (_isLadder)
        {
            _input = _playerInput.actions["LadderMove"].ReadValue<Vector2>();
        }
        else
        {
            _input = _playerInput.actions["GroundMove"].ReadValue<Vector2>();

        }
        _isLadder = GetComponent<PlayerController>().m_isLadder;
        _isClimbing = GetComponent<PlayerController>().m_isClimbing;

        Jump();
        Move();

        if (_isLadder && Mathf.Abs(PlayerMovementVector().y) > 0f)
        {
            GetComponent<PlayerController>().m_isClimbing = true;
        }

        if (_playerInput.actions["GroundMove"].IsInProgress() && m_remainingMoves != 0) m_isMoving = true;
        if (_playerInput.actions["GroundMove"].WasReleasedThisFrame()) m_isMoving = false;
    }

    private void FixedUpdate()
    {
        if (_isClimbing)
        {
            _rb.gravityScale = 0f;
            _rb.velocity = new Vector2(_rb.velocity.x, _move.y * _climbSpeed);
        }
        else
        {
            _rb.gravityScale = 1f;
        }
    }

    public Vector2 PlayerMovementVector()
    {
        return new Vector2(_input.x, _input.y);
    }

    void Move()
    {
        if (!CanMove()) return;

        if (!_isLadder)
        {
            _move = new Vector3(_input.x, 0);
            if (_input.x > 0)
            {
                _sr.flipX = false;
            }
            else if (_input.x < 0)
            {
                _sr.flipX = true;
            }
        }
        else
        {
            _move = new Vector3(_input.x, _input.y);
        }

        if (m_remainingMoves != 0)
        {
            transform.position += _walkSpeed * Time.deltaTime * _move;
            if (_playerInput.actions["GroundMove"].WasPressedThisFrame()) m_remainingMoves--;
        }
    }

    private bool CanMove()
    {
        bool canMove = true;
        if (m_remainingMoves <= 0) canMove = false;
        return canMove;
    }

    void Jump()
    {
        if (!CanJump()) return;

        if (_playerInput.actions["Jump"].WasPressedThisFrame() && IsGrounded())
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode2D.Impulse);
            m_remainingJumps--;
        }
    }

    public bool CanJump()
    {
        bool canJump = true;
        if (m_remainingJumps <= 0) canJump = false;
        return canJump;
    }

    public bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, _groundCheckerBoxSize, 0, -transform.up, _groundCheckerCastDistance, _groundLayer))
        {
            Debug.Log("Grounded");
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position - transform.up * _groundCheckerCastDistance, _groundCheckerBoxSize);
    }
}
