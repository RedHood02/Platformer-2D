using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlipSprite : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] PlayerController _playerController;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if(_playerController.PlayerMovementVector().x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if(_playerController.PlayerMovementVector().x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }


}
