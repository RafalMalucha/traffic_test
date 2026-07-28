using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestCarController : MonoBehaviour
{
    [SerializeField] private Rigidbody _carRigidbody;

    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float steering = 25f;
    [SerializeField] private float brakeForce = 25f;

    private float throttle;
    private float steeringInput;

    void Awake()
    {
        _carRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //float throttle = 0f;
        //float steeringInput = 0f;

        Drive(throttle);
        Steer(steeringInput);
    }

    private void Drive(float throttle)
    {
        if (throttle > 0f)
        {
            _carRigidbody.AddForce(
                transform.forward * throttle * acceleration,
                ForceMode.Force
            );
        }
        else if (throttle < 0f)
        {
            _carRigidbody.AddForce(
                transform.forward * throttle * brakeForce,
                ForceMode.Force
            );
        }

        Vector3 localVelocity = transform.InverseTransformDirection(_carRigidbody.linearVelocity);

        localVelocity.z = Mathf.Clamp(
            localVelocity.z,
            -maxSpeed * 0.5f,
            maxSpeed
        );

        _carRigidbody.linearVelocity = transform.TransformDirection(localVelocity);
    }

    private void Steer(float steeringInput)
    {
        if (_carRigidbody.linearVelocity.magnitude < 0.1f)
            return;

        float speedFactor = Mathf.Clamp01(
            _carRigidbody.linearVelocity.magnitude / maxSpeed
        );

        float rotation = steeringInput
                         * steering
                         * speedFactor
                         * Time.fixedDeltaTime;

        transform.Rotate(0f, rotation, 0f);
    }

    public void SetThrottle(float newThrottle)
    {
        throttle = newThrottle;
    }

    public void SetSteeringInput(float newSteeringInput)
    {
        steeringInput = newSteeringInput;
    }
}


