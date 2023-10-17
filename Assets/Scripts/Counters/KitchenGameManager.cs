using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{

    public static KitchenGameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;

    private enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver,
    }

    private State state;
    private float watingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer = 10f;

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                watingToStartTimer -= Time.deltaTime;
                if (watingToStartTimer < 0f)
                {
                    state = State.CountDownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }

                break;
            case State.CountDownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }

                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }

                break;
            case State.GameOver:
                break;
        }
    }

    public bool IsGamePlaying() => state == State.GamePlaying;

    public bool IsCountdownToStartActive() => state == State.CountDownToStart;

    public float GetCountdownToStartTimer() => countdownToStartTimer;
}
