using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool IsGameOver;
    public static bool IsGamePaused;
    public static int GasCanCounter;
    public static int TotalOilCans;
    public static float CurrentGasAmount;

    [SerializeField]
    private Slider _gasBar;
    [SerializeField]
    private GameObject _pausePanel;
    [SerializeField]
    private string _levelToLoad;

    public static UnityEvent LevelFinishedEvent = new UnityEvent();
    public static UnityEvent GameFailedEvent = new UnityEvent();

    private void OnEnable()
    {
        LevelFinishedEvent.AddListener(LevelComplete);
        GameFailedEvent.AddListener(GameOver);
    }

    private void Start()
    {
        IsGameOver = false;
        GasCanCounter = 0;
        CurrentGasAmount = 0;
        TotalOilCans = FindObjectsOfType<OilCan>().Length;
        IsGamePaused = false;
    }

    private void Update()
    {
        _gasBar.value = CurrentGasAmount;

        if (CurrentGasAmount <= 0 && TotalOilCans <= 0 && IsGameOver == false)
        {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.P) && IsGamePaused == true)
        {
            Time.timeScale = 1f;
            _pausePanel.SetActive(false);
            IsGamePaused = false;
        }
        else if (Input.GetKeyDown(KeyCode.P) && IsGamePaused == false)
        {
            Time.timeScale = 0f;
            _pausePanel.SetActive(true);
            IsGamePaused = true;
        }
    }

    private void LevelComplete()
    {
        IsGameOver = true;

    }

    private void GameOver()
    {
        IsGameOver = true;
    }
}