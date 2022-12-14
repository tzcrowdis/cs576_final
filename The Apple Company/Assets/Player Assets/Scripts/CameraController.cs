using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public Transform playerBody;

    private float xRotation;
    private float yRotation;

    public bool convo;
    public Transform target;
    public float turnSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        convo = false;
        
        xRotation = 0f;
        yRotation = 0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (convo == false)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            yRotation += mouseX;

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
            playerBody.rotation = Quaternion.Euler(0, yRotation, 0f);
            //playerBody.Rotate(0f, yRotation, 0f);
        }
        else
        {
            //rotate towards robot (from unity api)
            Vector3 targetDirection = target.position - transform.position;
            float singleStep = turnSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
}
