using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerWin : MonoBehaviour
{
    [SerializeField] private bool isKeyLevel;
    [SerializeField] SpriteRenderer doorRender;
    [SerializeField] Sprite closedRender, openRender;
    [SerializeField] Image blackOutImage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isKeyLevel)
        {
            if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PlayerKey>().GetHasKey())
            {
                Destroy(GameObject.FindGameObjectWithTag("Key"));
                doorRender.sprite = openRender;
                Debug.Log("Level Beat");
                StartCoroutine(BlackOut());
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                StartCoroutine(BlackOut());
                Debug.Log("Level Beat");
            }
        }
    }


    IEnumerator BlackOut()
    {
        StartCoroutine(LoadYourAsyncScene());
        while(blackOutImage.color.a != 0)
        {
            yield return new WaitForEndOfFrame();
            Color c = blackOutImage.color;
            c.a += 0.1f;
            blackOutImage.color = c;
        }
        yield break;
    }

    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
