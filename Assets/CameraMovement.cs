using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.5f;
    
    private Vector3 direction = Vector3.zero;

    void Update()
    {
        direction.x = Mathf.Sin(Time.time * moveSpeed) * 0.3f;  // Gerakan horizontal
        direction.z = Mathf.Cos(Time.time * moveSpeed) * 0.3f;  // Gerakan vertikal

        transform.position += direction * Time.deltaTime;
    }
}
