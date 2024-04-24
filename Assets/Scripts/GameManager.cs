using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public Vector2 playerPos, keyPos;

    public PlayerAnimationController controller;
    public LimitMovement limitMovement;

    private void Awake()
    {
        _instance = this;

        GameObject controller = GameObject.FindGameObjectWithTag("Player");

        playerPos = controller.transform.position;
        this.controller = controller.GetComponent<PlayerAnimationController>();

    }

    public void PlayerDeath()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Collider2D>().enabled = true;
        player.transform.position = playerPos;
        controller.ResetBool();
    }
}
