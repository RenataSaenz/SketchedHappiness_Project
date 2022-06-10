using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity = 100f;
    [SerializeField]
    private Transform playerBody;
    [SerializeField]
    private float xRotation = 0f;
    [SerializeField]
    private float yRotation = 0f;
    public int speed;

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //transform.Rotate(Vector3.up * mouseX);
        playerBody.Rotate(Vector3.up * mouseX);
        /*
        var mousePos = Input.mousePosition;

        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));
        Vector3 direction = worldMousePosition;
        gameObject.transform.position = Vector3.MoveTowards(transform.position, direction, speed * Time.deltaTime);*/
    }
}
