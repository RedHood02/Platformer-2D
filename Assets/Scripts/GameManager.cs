using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static string s_currentState;
    public static event System.Action<string> e_OnState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("ERROR: There's more than one GameManager");
        }

    }

    void Start()
    {
        s_currentState = "Gameplay";
        SwitchState(s_currentState);
    }


    public void SwitchState(string state)
    {
        s_currentState = state;
        e_OnState?.Invoke(s_currentState);
    }
}
