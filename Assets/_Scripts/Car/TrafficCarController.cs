using System.Collections.Generic;
using UnityEngine;

public class TrafficCarController : MonoBehaviour
{
    //[SerializeField] private Lane _currnetLane;
    [SerializeField] private SplineRoadLane _splineRoadLane;
    [SerializeField] private TestCarController _testCarController;
    
    private int _currentLaneNode;

    private void Update()
    {
        FollowLane();
    }

    private void FollowLane()
    {
        Vector3 target = _splineRoadLane.GetLaneNodes()[_currentLaneNode];

        Vector3 direction = target - transform.position;

        if (direction.magnitude < 3f)
        {
            _currentLaneNode++;

            if (_currentLaneNode >= _splineRoadLane.GetLaneNodes().Count)
            {
                _currentLaneNode = 0;
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

    public void SetNewLane(Lane newLane)
    {
        //_currnetLane = newLane;
    }

    public void SetNewSplineRoadLaneNodes(SplineRoadLane newSplineRoadLane)
    {
        _splineRoadLane = newSplineRoadLane;
    }
}
