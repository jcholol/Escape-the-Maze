using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMovement : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public Camera followCam;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 moveDirection = Vector3.up * movementSpeed * Time.deltaTime;
            transform.Translate(moveDirection);
            followCam.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 moveDirection = -transform.right * movementSpeed * Time.deltaTime;
            transform.Translate(moveDirection);
            followCam.transform.Translate(-transform.right * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 moveDirection = -Vector3.up * movementSpeed * Time.deltaTime;
            transform.Translate(moveDirection);
            followCam.transform.Translate(-Vector3.forward * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 moveDirection = transform.right * movementSpeed * Time.deltaTime;
            transform.Translate(moveDirection);
            followCam.transform.Translate(transform.right * movementSpeed * Time.deltaTime);
        }
        // Maintain constant Y position
        Vector3 newPosition = transform.position;
        newPosition.y = 1.5f;
        transform.position = newPosition;
    }
}
