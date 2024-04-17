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
    [SerializeField] Animator blackOutImage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isKeyLevel)
        {
            if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PlayerKey>().GetHasKey())
            {
                Destroy(GameObject.FindGameObjectWithTag("Key"));
                doorRender.sprite = openRender;
                Debug.Log("Level Beat");
                BlackOut();
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                BlackOut();
                Debug.Log("Level Beat");
            }
        }
    }


    void BlackOut()
    {
        blackOutImage.Play("FadeIn");
    }

    public bool GetIsKeyLevel()
    {
        return isKeyLevel;
    }
}
