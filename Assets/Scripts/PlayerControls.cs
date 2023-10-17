using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] InputAction firing;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float xRange = 5f;
    [SerializeField] float yRange = 5f;
    [SerializeField] GameObject[] lasers;
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float inputPitchFactor = -10f;
    [SerializeField] float positionYawFactor = -2f;
    [SerializeField] float inputRollFactor = -10f;

    // For input smoothing
    [SerializeField] float inputAccel = 0.2f;
    [SerializeField] float inputDragMultiplier = 0.8f;

    [SerializeField] float inputZeroEpsilon = 0.001f;

    Vector2 inputVector = Vector2.zero;
    Vector2 inputVelocity = Vector2.zero;

    bool fireInput = false;

    void OnEnable()
    {
        movement.Enable();
        firing.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
        firing.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessTranslation()
    {
        inputVector = movement.ReadValue<Vector2>();

        // X Drag
        if (Mathf.Abs(inputVelocity.x) <= inputZeroEpsilon)
        {
            inputVelocity.x = 0;
        }

        else 
        {
            inputVelocity.x *= inputDragMultiplier;
        }

        // X Accel
        inputVelocity.x = Mathf.Clamp(inputVelocity.x + inputVector.x * inputAccel, -1, 1);

        // Y Drag
        if (Mathf.Abs(inputVelocity.y) <= inputZeroEpsilon)
        {
            inputVelocity.y = 0;
        }

        else 
        {
            inputVelocity.y *= inputDragMultiplier;
        }

        // Y Accel
        inputVelocity.y = Mathf.Clamp(inputVelocity.y + inputVector.y * inputAccel, -1, 1);

        float newX = Mathf.Clamp(transform.localPosition.x + inputVelocity.x * moveSpeed * Time.deltaTime, -xRange, xRange);
        float newY = Mathf.Clamp(transform.localPosition.y + inputVelocity.y * moveSpeed * Time.deltaTime, -yRange, yRange);

        transform.localPosition = new Vector3(
            newX,
            newY,
            transform.localPosition.z
        );
    }

    private void ProcessRotation()
    {
        float pitch = (transform.localPosition.y * positionPitchFactor) + (inputVelocity.y * inputPitchFactor);
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = inputVelocity.x * inputRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessFiring() {
        if (firing.IsPressed())
        {
            ActivateLasers();
        }

        else
        {
            DeactivateLasers();
        }
    }

    private void ActivateLasers()
    {
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(true);
        }
    }

    private void DeactivateLasers()
    {
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(false);
        }
    }
}
