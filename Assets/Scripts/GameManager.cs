using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool IsGameOver;
    public static bool IsGamePaused;
    public static bool CanQuit;
    public static int GasCanCounter;
    public static int TotalOilCans;
    public static float CurrentGasAmount;

    [SerializeField]
    private Slider _gasBar;
    [SerializeField]
    private GameObject _pausePanel;
    [SerializeField]
    private string _levelToLoad;
    [SerializeField]
    private GameObject _levelFailedText;
    [SerializeField]
    private GameObject _levelSuccessText;

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
        IsGamePaused = false;
        CanQuit = false;
        GasCanCounter = 0;
        CurrentGasAmount = 0;
        TotalOilCans = FindObjectsOfType<OilCan>().Length;
    }

    private void Update()
    {
        _gasBar.value = CurrentGasAmount;

        if (CurrentGasAmount <= 0 && TotalOilCans <= 0 && IsGameOver == false)
        {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.N) && IsGamePaused == true)
        {
            Time.timeScale = 1f;
            _pausePanel.SetActive(false);
            IsGamePaused = false;
            CanQuit = false;
        }
        else if (Input.GetKeyDown(KeyCode.N) && IsGamePaused == false)
        {
            Time.timeScale = 0f;
            _pausePanel.SetActive(true);
            IsGamePaused = true;
            CanQuit = true;
        }

        if (Input.GetKeyDown(KeyCode.Y) && CanQuit == true)
        {
            SceneManager.LoadScene("MainMenu");
        }
        

    }

    private void LevelComplete()
    {
        IsGameOver = true;
        if (SceneManager.GetActiveScene().name == "Level2")
        {
            _levelSuccessText.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(_levelToLoad);
        }
    }

    private void GameOver()
    {
        IsGameOver = true;
        _levelFailedText.SetActive(true);
    }
}