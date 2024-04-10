using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWin : MonoBehaviour
{
    [SerializeField] private bool isKeyLevel;
    [SerializeField] SpriteRenderer doorRender;
    [SerializeField] Sprite closedRender, openRender;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isKeyLevel)
        {
            if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PlayerKey>().GetHasKey())
            {
                Destroy(GameObject.FindGameObjectWithTag("Key"));
                doorRender.sprite = openRender;
                Debug.Log("Level Beat");
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Level Beat");
            }
        }
    }
}
