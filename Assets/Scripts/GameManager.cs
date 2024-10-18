using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //Events
    public static event System.Action<GameState> OnStateChange;

    //Variables
    [Header("Settings")]
    [SerializeField] private GameState _initialState;

    //Properties
    public static GameState s_currentState { get; private set; } = (GameState)(-1);
    protected override bool Persistent => false;


    void Start()
    {
        SwitchState(_initialState);
    }


    public static void SwitchState(GameState state)
    {
        if (s_currentState == state) return;

        s_currentState = state;
        OnStateChange?.Invoke(s_currentState);
    }
}

public enum GameState { Menu, Gameplay, GameOver }
