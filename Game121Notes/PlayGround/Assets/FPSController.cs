using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{

    public float moveSpeed;
    private CharacterController characterController;
    private Transform cameraTransform;

    public float rotationSpeed;

    public bool invertYAxis = false;

    private float currentTilt = 0;
    public float maxTiltAngle = 45;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        movement.Normalize();

        characterController.SimpleMove(movement * moveSpeed);

        //Mouse Movement rotation
        //Main Character rotation horizontally
        float mouseXDelta = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, mouseXDelta * rotationSpeed, Space.Self);

        //Camera Move Vertically
        float mouseYDelta = Input.GetAxis("Mouse Y");
        if (invertYAxis)
        {
            mouseYDelta *= -1;
        }

        Quaternion originalCameraRotation = cameraTransform.rotation;

        cameraTransform.Rotate(Vector3.right, mouseYDelta * rotationSpeed, Space.Self);

        bool isInvalidRotation = Vector3.Angle(cameraTransform.forward, transform.forward) > maxTiltAngle; //will check if it's valid or invalid

        if(isInvalidRotation)
        {
            cameraTransform.rotation = originalCameraRotation;
        }

    }

    private void OnGUI()
    {
        GUILayout.Label(currentTilt.ToString("0.00"));
    }
}
