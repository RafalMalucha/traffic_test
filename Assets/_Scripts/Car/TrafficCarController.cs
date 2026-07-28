using UnityEngine;

public class TrafficCarController : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private TestCarController _testCarController;
    
    private int _currentWaypoint;

    private void Update()
    {
        FollowLane();
    }

    private void FollowLane()
    {
        Transform target = _waypoints[_currentWaypoint];

        Vector3 direction = target.position - transform.position;

        if (direction.magnitude < 3f)
        {
            _currentWaypoint++;

            if (_currentWaypoint >= _waypoints.Length)
            {
                _currentWaypoint = 0;
            }

            return;
        }

        Vector3 localDirection = transform.InverseTransformDirection(direction.normalized);

        // -1 = left
        //  0 = straight
        // +1 = right
        float steeringInput = localDirection.x;

        _testCarController.SetSteeringInput(steeringInput);

        _testCarController.SetThrottle(1f);
    }
}
