using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private Transform _playerKeySpot;
    [SerializeField] private Vector2 _keyStartPos;
    [SerializeField] private float _moveSpeed;


    private void Awake()
    {
        _keyStartPos = transform.position;
        _playerKeySpot = GameObject.FindGameObjectWithTag("PlayerKeySpot").transform;
    }

    private void Update()
    {
        if (GameplayManager.Instance.m_isPlayerHasKey)
        {
            transform.position = Vector2.MoveTowards(transform.position, _playerKeySpot.position, _moveSpeed * Time.deltaTime);
        }

        if (GameplayManager.Instance.m_isGameOver)
        {
            ResetKey();
        }
    }

    public void ResetKey()
    {
        GameplayManager.Instance.m_isPlayerHasKey = false;
        transform.position = _keyStartPos;
    }
}
