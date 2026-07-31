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
        Vector3[] rayOffsets =
        {
            Vector3.zero,
            Vector3.right,
            Vector3.left
        };

        foreach (Vector3 offset in rayOffsets)
        {
            Vector3 origin = transform.TransformPoint(offset);

            Debug.DrawRay(origin, transform.forward * 4f, Color.green);

            if (Physics.Raycast(origin, transform.forward, out RaycastHit hit, 4f))
            {
                if (!hit.collider.CompareTag("TrafficTrigger"))
                {
                    _testCarController.SetThrottle(0f);
                    return;
                }
            }
        }
    }

    public void SetNewLane(Lane newLane)
    {
        //_currnetLane = newLane;
    }

    public void SetNewSplineRoadLaneNodes(SplineRoadLane newSplineRoadLane)
    {
        _currentLaneNode = 0;
        _splineRoadLane = newSplineRoadLane;
    }
}
