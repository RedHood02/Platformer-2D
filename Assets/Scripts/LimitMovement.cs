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
        if(playerMov.GetCanPlayerMove() == true)
        {
            ReduceLimit();
            UpdateTMP();

            if(currentStrokesLeft == 0 && currentJumpsLeft == 0)
            {
                StartCoroutine(PlayerLoss());
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

    void UpdateTMP()
    {
        jumpText.text = currentJumpsLeft.ToString("0");
        walkText.text = currentStrokesLeft.ToString("0");
    }

    IEnumerator PlayerLoss()
    {
        playerMov.SetCanPlayerMove(false);
        //play death animation
        yield return new WaitForSeconds(1f);
        //Replace death scene for relocating player
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        yield break;
    }
}
