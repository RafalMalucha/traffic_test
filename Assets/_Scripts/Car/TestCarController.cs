using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestCarController : MonoBehaviour
{
    [SerializeField] private InputActionAsset _inputManager;
    [SerializeField] private Rigidbody _carRigidbody;

    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float steering = 25f;
    [SerializeField] private float brakeForce = 25f;

    private Quaternion _initRotation;
    private Quaternion _targetDirection;
    private float _targetSpeed;

    private float _speedInterpolationStrength = 5f;
    private float _rotationInterpolationStrength = 100f; 

    private InputAction _accel;
    private InputAction _break;
    private InputAction _turnLeft;
    private InputAction _turnRight;

    private void OnEnable() 
    {
        _inputManager.FindActionMap("Player").Enable();
    }

    private void OnDisable() 
    {
        _inputManager.FindActionMap("Player").Disable();
    }
    void Awake()
    {
        _inputManager.FindActionMap("Player").Enable();

        _accel = InputSystem.actions.FindAction("Car_Accel");
        _break = InputSystem.actions.FindAction("Car_Break");
        _turnLeft = InputSystem.actions.FindAction("Car_TurnLeft");
        _turnRight = InputSystem.actions.FindAction("Car_TurnRight");

        _carRigidbody = GetComponent<Rigidbody>();

        _initRotation = _carRigidbody.rotation * Quaternion.identity;
    }

    void Update()
    {
        float vertical = 0f;
        float horizontal = 0f;

        if(_accel.ReadValue<float>() > 0)
        {
            vertical += 1f;
        }
        if(_break.ReadValue<float>() > 0)
        {
            vertical += -1f;
        }

        if(_turnRight.ReadValue<float>() > 0)
        {
            horizontal += 1f;
        }
        if(_turnLeft.ReadValue<float>() > 0)
        {
            horizontal += -1f;
        }

        Drive(vertical);
        Steer(horizontal);
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
}


