using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] Transform playerKeySpot;
    [SerializeField] Vector2 keySpawnPos;

    public bool isPickedUp;
    [SerializeField] float moveSpeed;

    private void Awake()
    {
        keySpawnPos = transform.position;
        playerKeySpot = GameObject.FindGameObjectWithTag("PlayerKeySpot").transform;
    }


    private void Update()
    {
        if(isPickedUp)
        {
           transform.position = Vector2.MoveTowards(transform.position, playerKeySpot.position, moveSpeed * Time.deltaTime);
        }
    }

    public void ResetKey()
    {
        isPickedUp = false;
        transform.position = keySpawnPos;
    }
}
