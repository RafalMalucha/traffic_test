using UnityEngine;

public class IntersectionExitTrigger : MonoBehaviour
{
    [SerializeField] private SplineRoadLane _lane;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Traffic"))
        {
            collider.GetComponent<TrafficCarController>().SetNewSplineRoadLaneNodes(_lane);
            //collider.GetComponent<TestCarController>().SetNewMaxSpeed(3f);
        }
    }
}
