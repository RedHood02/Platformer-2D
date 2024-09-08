using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance { get; private set; }

    [Header("Movement Counter")]
    public float m_remainingJumps;
    public float m_maxJumps;
    public float m_remainingMoves;
    public float m_maxMoves;

    [Header("HUD")]
    [SerializeField] private TMP_Text _jumpCounter;
    [SerializeField] private TMP_Text _moveCounter;

    [Header("Key")]
    public bool m_isLevelKey;
    public bool m_isPlayerHasKey;

    [Header("Gameplay States")]
    public bool m_isGameOver;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("ERROR: There's more than one GameplayManager");
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.SwitchState("Gameplay");
    }

    private void HandleChangeState(string currentState)
    {
        if (currentState == "GameOver")
        {
            m_isGameOver = true;
            ResetLevel();
        }
    }

    private void Update()
    {
        UpdateText();
        if (m_remainingJumps == 0 && m_remainingMoves == 0) HandleChangeState("m_isGameOver");
    }

    public void ResetLevel()
    {
        m_remainingMoves = m_maxMoves;
        m_remainingJumps = m_maxJumps;
        m_isGameOver = false;
    }

    void UpdateText()
    {
        _moveCounter.text = m_remainingMoves.ToString("0");
        _jumpCounter.text = m_remainingJumps.ToString("0");
    }
}