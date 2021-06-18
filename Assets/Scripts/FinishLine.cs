using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TargetCar"))
        {
            GameManager.LevelFinishedEvent.Invoke();
        }
    }
}