using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoor : MonoBehaviour
{
    [SerializeField] Animator blackOutAnimator;

    private void Awake()
    {
        blackOutAnimator = GameObject.FindGameObjectWithTag("BlackOut").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            blackOutAnimator.Play("FadeIn");
        }
    }
}
