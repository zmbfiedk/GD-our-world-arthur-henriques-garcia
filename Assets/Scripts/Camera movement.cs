using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerBody; 
    public float mouseSensitivity = 100000f;
    public float verticalRotationLimit = 100000f; 

    private float xRotation = 0f;
    private float yRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; 
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; 

        xRotation -= mouseY; 
        xRotation = Mathf.Clamp(xRotation, -verticalRotationLimit, verticalRotationLimit); 

        
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f); 
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); 

        
        yRotation += mouseX; 
        playerBody.localRotation = Quaternion.Euler(0f, yRotation, 0f); 
        playerBody.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}