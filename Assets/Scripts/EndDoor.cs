using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoor : MonoBehaviour
{
    [SerializeField] Animator blackOutAnimator;
    [SerializeField] bool isKeyLevel, playerHasKey;
    private void Awake()
    {
        blackOutAnimator = GameObject.FindGameObjectWithTag("BlackOut").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isKeyLevel && !playerHasKey) return;

        if(collision.gameObject.CompareTag("Player"))
        {
            blackOutAnimator.Play("FadeIn");
        }
    }

    public void SetPlayerHasKey(bool newBool)
    {
        playerHasKey = newBool;
    }

    public bool GetIsKeyLevel()
    {
        return isKeyLevel;
    }
}
