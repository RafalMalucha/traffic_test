using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TestCarController))]
public class TrafficCarController : MonoBehaviour
{
    //[SerializeField] private Lane _currnetLane;
    [SerializeField] private SplineRoadLane _splineRoadLane;
    [SerializeField] private TestCarController _testCarController;

    private bool _recentlyParked = false;

    private int _currentLaneNode;

    private void Start()
    {
        _testCarController = this.GetComponent<TestCarController>();
        _testCarController.SetNewMaxSpeed(Random.Range(2f, 4f));
    }

    private void Update()
    {
        if (_splineRoadLane)
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

            Debug.DrawRay(origin, transform.forward * 5f, Color.green);

            if (Physics.Raycast(origin, transform.forward, out RaycastHit hit, 5f))
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

    public void SetRecentlyParked(bool newValue)
    {
        _recentlyParked = newValue;
        StartCoroutine(ResetRecentlyParked());
    }

    public bool GetRecentlyParked()
    {
        return _recentlyParked;
    }

    IEnumerator ResetRecentlyParked()
    {
        yield return new WaitForSeconds(Random.Range(60, 90));

        _recentlyParked = false;
    }
}
