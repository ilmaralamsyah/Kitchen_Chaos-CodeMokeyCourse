using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;


    [SerializeField] private float gamePlayingTimerMax = 210f;
    private float gamePlayingTimer;

    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private State state;

    private float waitinToStartTimer = 0.1f;
    private float CountdownToStartTimer = 3f;
    private bool isPaused;
    private int plusPoint = 10;
    private int minusPoint = 5;
    private int totalPoint = 0;


    private void Awake()
    {
        Instance = this;

        state = State.WaitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPause += GameInput_OnPause;
        DeliveryManager.Instance.TotalIngredientOrderDelivered += DeliveryManager_TotalIngredientOrderDelivered;
    }

    private void DeliveryManager_TotalIngredientOrderDelivered(object sender, DeliveryManager.TotalIngredientOrderSuccessEventArgs e)
    {
        if(e.totalIngredientOrder >= 0)
        {
            totalPoint += plusPoint * e.totalIngredientOrder;
        }
        else
        {
            totalPoint += minusPoint * e.totalIngredientOrder;
        }
        
    }

    private void GameInput_OnPause(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitinToStartTimer -= Time.deltaTime;
                if (waitinToStartTimer < 0)
                {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountdownToStart:
                CountdownToStartTimer -= Time.deltaTime;
                if (CountdownToStartTimer < 0)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                
                break;
        }
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return CountdownToStartTimer;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetGamePlayingTimerNormalized()
    {
        return gamePlayingTimer/gamePlayingTimerMax;
    }

    public string GetGamePlayingTimerFormatted()
    {
        float minutes = Mathf.FloorToInt(gamePlayingTimer / 60);
        float seconds = Mathf.FloorToInt(gamePlayingTimer % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void TogglePauseGame()
    {
        isPaused = !isPaused;
        if(isPaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnPaused?.Invoke(this, EventArgs.Empty);
        }
    }

    public int GetTotalPoint()
    {
        return totalPoint;
    }
}
