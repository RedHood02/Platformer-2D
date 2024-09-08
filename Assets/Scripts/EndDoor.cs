using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoor : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite _doorOpen;
    [SerializeField] private Sprite _doorClose;
    private SpriteRenderer _sr;

    [Header("Finish Level")]
    [SerializeField] private Animator _blackOutAnimator;


    private void Awake()
    {
        _blackOutAnimator = GameObject.FindGameObjectWithTag("BlackOut").GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (GameplayManager.Instance.m_isLevelKey)
        {
            _sr.sprite = _doorClose;
        }
        else
        {
            _sr.sprite = _doorOpen;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameplayManager.Instance.m_isLevelKey && !GameplayManager.Instance.m_isPlayerHasKey) return;

        if (collision.gameObject.CompareTag("Player")) _blackOutAnimator.Play("FadeIn");
    }
}
