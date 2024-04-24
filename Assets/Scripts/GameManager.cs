using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public Vector2 playerPos, keyPos;

    public PlayerAnimationController controller;
    public LimitMovement limitMovement;
    public Key keyController;
    public EndDoor endDoorController;
    private void Awake()
    {
        _instance = this;

        GameObject controller = GameObject.FindGameObjectWithTag("Player");
        playerPos = controller.transform.position;
        this.controller = controller.GetComponent<PlayerAnimationController>();
        keyController = FindObjectOfType<Key>();
        endDoorController = FindObjectOfType<EndDoor>();
    }

    private void Update()
    {
        if (!endDoorController.GetIsKeyLevel()) return;
    }
    public void SetPlayerHasKey(bool newBool)
    {
        endDoorController.SetPlayerHasKey(newBool);

    }

    public void PlayerDeath()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        limitMovement = player.GetComponent<LimitMovement>();
        player.GetComponent<Collider2D>().enabled = true;
        player.transform.position = playerPos;
        player.GetComponent<PlayerController>().lastKeyStroke = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        limitMovement.ResetNumbers();
        if(endDoorController.GetIsKeyLevel())
        {
            keyController.ResetKey();
        }
        controller.ResetBool();
    }
}
