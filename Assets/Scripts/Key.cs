using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    [SerializeField] Transform playerSpot;

    [SerializeField] bool isPickedUp;
    [SerializeField] float speed;

    private void Update()
    {
        PickUp();
    }

    public void PickUp()
    {
        if(isPickedUp)
        {
            var step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, playerSpot.transform.position, speed);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerKey>().SetHasKey(true);
            isPickedUp = true;
        }
    }

    public bool GetIsPickedUp()
    {
        return isPickedUp;
    }
}
