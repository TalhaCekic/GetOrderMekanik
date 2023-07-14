using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float rotationSpeed = 10f; // Dönüþ hýzý
    public Transform rotationCenter; // Dönüþ merkezi

    private void Update()
    {
        transform.RotateAround(rotationCenter.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
