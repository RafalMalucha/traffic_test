using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCarController : MonoBehaviour
{
    [SerializeField] private InputActionAsset _inputManager;
    [SerializeField] private TestCarController _testCarController;
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

        _testCarController.SetThrottle(vertical);
        _testCarController.SetSteeringInput(horizontal);
    }
}
