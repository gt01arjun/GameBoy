using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool GameOver;
    public static int GasCanCounter;
    public static int TotalOilCans;
    public static float CurrentGasAmount;

    [SerializeField]
    private Slider _gasBar;

    private void Start()
    {
        GameOver = false;
        GasCanCounter = 0;
        CurrentGasAmount = 0;
        TotalOilCans = FindObjectsOfType<OilCan>().Length;
    }

    private void Update()
    {
        _gasBar.value = CurrentGasAmount;

        if (CurrentGasAmount <= 0 && TotalOilCans <= 0)
        {
            Debug.Log("GameOver");
        }
    }
}