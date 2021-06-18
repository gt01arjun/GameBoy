using UnityEngine;

public class OilCan : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() || collision.gameObject.GetComponent<CarMovement>())
        {
            GameManager.GasCanCounter++;
            Destroy(gameObject);
        }
    }
}