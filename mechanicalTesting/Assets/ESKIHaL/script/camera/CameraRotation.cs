using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float rotationSpeed = 10f; // D�n�� h�z�
    public Transform rotationCenter; // D�n�� merkezi

    private void Update()
    {
        transform.RotateAround(rotationCenter.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
