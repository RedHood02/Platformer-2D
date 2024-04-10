using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelIntro : MonoBehaviour
{
    [SerializeField] GameObject playerCamera, levelCamera;
    [SerializeField] float waitTime;

    private void Start()
    {
        StartCoroutine(CameraPan(waitTime));
    }



    IEnumerator CameraPan(float wait)
    {
        playerCamera.SetActive(false);
        levelCamera.SetActive(true);
        yield return new WaitForSeconds(wait);
        levelCamera.SetActive(false);
        playerCamera.SetActive(true);

    }
}
