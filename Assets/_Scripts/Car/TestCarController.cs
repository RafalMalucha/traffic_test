using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TestCarController : MonoBehaviour
{
    [SerializeField] private Rigidbody _carRigidbody;

    [SerializeField] private float _acceleration = 10f;
    [SerializeField] private float _maxSpeed = 20f;
    [SerializeField] private float _steering = 25f;
    [SerializeField] private float _brakeForce = 25f;

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
                 throttle * _acceleration * transform.forward,
                ForceMode.Force
            );
        }
        else if (throttle < 0f)
        {
            _carRigidbody.AddForce(
                throttle * _brakeForce * transform.forward,
                ForceMode.Force
            );
        }

        Vector3 localVelocity = transform.InverseTransformDirection(_carRigidbody.linearVelocity);

        localVelocity.z = Mathf.Clamp(
            localVelocity.z,
            -_maxSpeed * 0.5f,
            _maxSpeed
        );

        _carRigidbody.linearVelocity = transform.TransformDirection(localVelocity);
    }

    private void Steer(float steeringInput)
    {
        if (_carRigidbody.linearVelocity.magnitude < 0.1f)
            return;

        float speedFactor = Mathf.Clamp01(
            _carRigidbody.linearVelocity.magnitude / _maxSpeed
        );

        float rotation = steeringInput
                         * _steering
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

    public void SetNewMaxSpeed(float newMaxSpeed)
    {
        _maxSpeed = newMaxSpeed;
    }
}
