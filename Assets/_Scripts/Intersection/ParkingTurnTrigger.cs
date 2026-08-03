using UnityEngine;

public class ParkingTurnTrigger : MonoBehaviour
{
    [SerializeField] private SplineRoadLane _lane;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Traffic"))
        {
            int randomTurn = Random.Range(0, 3);
            if (randomTurn == 1)
            {
                collider.GetComponent<TrafficCarController>().SetNewSplineRoadLaneNodes(_lane);
                //collider.GetComponent<TestCarController>().SetNewMaxSpeed(3f);
            }
        }
    }
}
