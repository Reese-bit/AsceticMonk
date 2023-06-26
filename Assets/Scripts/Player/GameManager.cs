using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistenSingleton<GameManager>
{
    public static Action onGameOver;
    public static GameState GameState { get => Instance.gameState; set => Instance.gameState = value; }
    [SerializeField] private GameState gameState = GameState.Playing;

    private void Update()
    {
        if (gameState == GameState.Menu)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (gameState == GameState.Playing)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (gameState == GameState.GameOver)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (gameState == GameState.Win)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (gameState == GameState.Lose)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}

public enum GameState
{
    Menu,
    Playing,
    GameOver,
    Win,
    Lose
}
