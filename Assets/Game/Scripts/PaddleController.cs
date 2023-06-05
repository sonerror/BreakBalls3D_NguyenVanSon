using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PaddleController : Singleton<PaddleController>
{
    private float currentRotation = 0f;
    private float targetRotation = 90f;
    private Quaternion initialRotation;

    public float moveSpeed;
    public float rotationSpeed;


    public float minX;
    public float maxX;

    private bool isRotating = false;
    private bool isMoving = false;

    public bool isInsBall =  false;
    private void Start()
    {
        initialRotation = transform.rotation;
    }
    private void Update()
    {
        RotationHand();
    }

    private void RotationHand()
    {
        if (Input.GetMouseButton(0))
        {
            isMoving = true;

            isRotating = true;
        }
        else
        {
            currentRotation = 0f;

            isRotating = false;

            isMoving = false;

        }
        if (isRotating)
        {
            currentRotation = Mathf.Lerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.rotation = Quaternion.Euler(0f, 0f, -currentRotation);

            MoveHand();
            if (currentRotation > 80)
            {
               isInsBall = true;
            }

        }
        else
        {
            isInsBall = false;

            transform.rotation = initialRotation;
        }
    }
    void MoveHand()
    {
       if(isMoving)
        {
            Vector3 mouseDelta = new Vector3(Input.GetAxis("Mouse X"), 0f, 0f);

            float newPositionX = transform.position.x + mouseDelta.x * moveSpeed * Time.deltaTime;

            float clampedPositionX = Mathf.Clamp(newPositionX, minX, maxX);

            transform.position = new Vector3(clampedPositionX, transform.position.y, transform.position.z);
/*
            rootBalls.transform.position = new Vector3(clampedPositionX, transform.position.y, transform.position.z);
            rootBalls.gameObject.transform.SetParent(null);*/
        }
    }
}