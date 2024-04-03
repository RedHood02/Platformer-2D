using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LimitMovement : MonoBehaviour
{
    [SerializeField] float maxMovementStrokes, currentStrokesLeft;
    [SerializeField] float maxJumps, currentJumpsLeft;
    [SerializeField] TMP_Text jumpText, walkText;

    [SerializeField] PlayerMovement playerMov;


    private void Awake()
    {
        currentStrokesLeft = maxMovementStrokes;
        currentJumpsLeft = maxJumps;
        playerMov = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }


    private void Update()
    {
        if (playerMov.GetCanPlayerMove() == true)
        {
            ReduceLimit();
            UpdateTMP();
            if (currentStrokesLeft <= 0 && currentJumpsLeft <= 0)
            {
                StartCoroutine(GracePeriod());
            }
        }
    }


    void ReduceLimit()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentStrokesLeft--;
        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            currentStrokesLeft--;
        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            currentStrokesLeft--;
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            currentStrokesLeft--;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentJumpsLeft--;
        }
    }

    float ClampJumps()
    {
        float jumpsLeftClamp = Mathf.Clamp(currentJumpsLeft, 0, maxJumps);
        return jumpsLeftClamp;
    }

    float ClampStrokes()
    {
        float moveStrokesLeftClamp = Mathf.Clamp(currentStrokesLeft, 0, maxMovementStrokes);
        return moveStrokesLeftClamp;
    }

    void UpdateTMP()
    {
        jumpText.text = ClampJumps().ToString("0");
        walkText.text = ClampStrokes().ToString("0");
    }

    IEnumerator GracePeriod()
    {
        //play death animation
        yield return new WaitForSeconds(1.25f);
        //Replace death scene for relocating player
        playerMov.SetCanPlayerMove(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        yield break;
    }
}
