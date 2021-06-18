using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform TargetToFollow;

    public float SmoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        Vector3 goalPos = TargetToFollow.position;
        goalPos.z = -10f;
        transform.position = Vector3.SmoothDamp(transform.position, goalPos, ref velocity, SmoothTime);
    }
}