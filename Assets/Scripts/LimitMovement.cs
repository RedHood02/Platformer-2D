using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LimitMovement : MonoBehaviour
{
    [SerializeField] PlayerController _playerController;

    public float currentKeyStrokesLeft, maxKeyStrokes;
    public float currentJumpsLeft, maxJumps;
    [SerializeField] TMP_Text jumpText, walkText;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        currentKeyStrokesLeft = maxKeyStrokes;
        currentJumpsLeft = maxJumps;
    }

    private void Update()
    {
        LimitMov();
        LimitJumps();
        UpdateText();
    }

    void LimitMov()
    {
        if (currentKeyStrokesLeft == 0 && (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W)))
        {
            Debug.Log("Last Key");
            _playerController.lastKeyStroke = true;
        }
        if (currentKeyStrokesLeft <= 0) return;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
        {
            currentKeyStrokesLeft--;
        }
    }

    void UpdateText()
    {
        walkText.text = currentKeyStrokesLeft.ToString("0");
        jumpText.text = currentJumpsLeft.ToString("0");
    }

    void LimitJumps()
    {
        if (currentJumpsLeft <= 0) return;

        if (Input.GetKeyUp(KeyCode.Space))
        {
            currentJumpsLeft--;
        }
    }

    public void ResetNumbers()
    {
        currentKeyStrokesLeft = maxKeyStrokes;
        currentJumpsLeft = maxJumps;
    }

    public bool CanJump()
    {
        bool canJump = true;
        if (currentJumpsLeft <= 0)
        {
            canJump = false;
        }
        return canJump;
    }
}
