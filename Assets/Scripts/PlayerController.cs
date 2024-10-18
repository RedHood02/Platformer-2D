using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Vector2 _playerStartPos;

    [Header("Ladder")]
    public bool m_isLadder;
    public bool m_isClimbing;

    [Header("Key")]
    public bool m_isLevelKey;
    public bool m_isPlayerHasKey;

    [Header("Animation")]
    [SerializeField] private Animator _anim;
    public bool m_isDrowning = false;


    void Start()
    {
        _playerStartPos = gameObject.transform.position;
    }

    void Update()
    {
        //if (GameplayManager.Instance.m_isGameOver) Death();
        _anim.SetBool("IsGrounded", GetComponent<PlayerMovement>().IsGrounded());
        _anim.SetBool("IsMoving", GetComponent<PlayerMovement>().m_isMoving);
        _anim.SetBool("IsDrowning", m_isDrowning);
        _anim.SetBool("InLadder", m_isLadder);

        if (_anim.GetBool("InLadder") && !m_isClimbing)
        {
            _anim.speed = 0;
        }
        else if (_anim.GetBool("InLadder") && m_isClimbing)
        {
            _anim.speed = 1;
        }
        else
        {
            _anim.speed = 1;
        }

    }

    void Death()
    {
        GetComponent<Collider2D>().enabled = true;
        transform.position = _playerStartPos;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Water"))
        {
            StartCoroutine(PlayerDrownDeath());
        }
    }

    IEnumerator PlayerDrownDeath()
    {
        m_isDrowning = true;
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(1.25f);
        Death();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            m_isLadder = true;
        }

        if (collision.CompareTag("Key"))
        {
            m_isPlayerHasKey = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            m_isLadder = false;
            m_isClimbing = false;
        }
    }
}