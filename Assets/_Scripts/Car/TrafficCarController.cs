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
        if(_splineRoadLane)
        {
            FollowLane();
        }
        else
        {
            NoLaneJustGoForward();
        }

        HandleObstacles();
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

    private void NoLaneJustGoForward()
    {
        _testCarController.SetThrottle(1f);
    }

    private void HandleObstacles()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit _raycastHit;

        Debug.DrawRay(ray.origin, ray.direction * 5, Color.green);

        if (Physics.Raycast(ray, out _raycastHit, 5))
        {
            if (_raycastHit.collider.tag != "TrafficTrigger")
            {
                _testCarController.SetThrottle(0f);
            }
        }
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
