using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour {

    public static KitchenGameManager Instance { get; private set; }


    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;


    private enum State {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private State state;
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gameplayingTimer;
    private float gameplayingTimerMax = 10f;
    private bool isGamePause = false;

    private void Awake() {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Start() {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e) {
        TogglePauseGame();
    }

    private void Update() {
        switch (state) {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f) {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, new EventArgs());
                }
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f) {
                    state = State.GamePlaying;
                    gameplayingTimer = gameplayingTimerMax;
                    OnStateChanged?.Invoke(this, new EventArgs());
                }
                break;
            case State.GamePlaying:
                gameplayingTimer -= Time.deltaTime;
                if (gameplayingTimer < 0f) {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, new EventArgs());
                }
                break;
            case State.GameOver:
                break;
        }
    }

    public bool IsGamePlaying() {
        return state == State.GamePlaying;
    }

    public bool IsCountdowmToStartActive() {
        return state == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer() {
        return countdownToStartTimer;
    }

    public bool IsGameOver() {
        return state == State.GameOver;
    }

    public float GetPlayingTimerNormalized() {
        return 1 - (gameplayingTimer / gameplayingTimerMax);
    }

    public void TogglePauseGame() {
        isGamePause = !isGamePause;

        if (isGamePause) {
            Time.timeScale = 0f;

            OnGamePaused?.Invoke(this, EventArgs.Empty);
        } else {
            Time.timeScale = 1f;

            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }


}
