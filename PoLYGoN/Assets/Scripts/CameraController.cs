using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    public Transform playerBody;

    public Vector3 offset;
    private float currentZoom = 5f;
    public float pitch = 3f;

    public float zoomSpeed =4f;
    public float minzoom=5f;
    public float maxzoom=15f;

    public float sensitivity = 100f;
    private float currentYaw = 0f;


    private float xRotation=0f;



    // Start is called before the first frame update
    void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom,minzoom,maxzoom);

        currentYaw  += Input.GetAxis("Mouse X") * sensitivity *Time.deltaTime;


        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        
        xRotation -= mouseY;
        xRotation= Mathf.Clamp(xRotation,-90f,90f);

        playerBody.Rotate(Vector3.up * mouseX);


    }


    void LateUpdate()
    {
        transform.position = playerBody.position - offset * currentZoom;
        transform.LookAt(playerBody.position + Vector3.up *pitch);
        transform.RotateAround(playerBody.position, Vector3.up, currentYaw);
        transform.localRotation = Quaternion.Euler(xRotation,0,0);
    }
    
    


}
