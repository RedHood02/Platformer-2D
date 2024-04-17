using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMov;
    
    private void Awake()
    {
        playerMov = GetComponent<PlayerMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Water"))
        {
            StartCoroutine(playerMov.DrowningDeath());
        }
    }
}
