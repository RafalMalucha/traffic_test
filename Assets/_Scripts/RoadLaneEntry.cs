using UnityEngine;

public class RoadLaneEntry : MonoBehaviour
{
    [SerializeField] private SplineRoadLane[] _availableLanes;
    
    private void OnTriggerEnter(Collider collider) 
    {
        if (collider.tag == "Traffic")
        {
            int randomLaneIndex = Random.Range(0, _availableLanes.Length);

            switch(_availableLanes[randomLaneIndex].GetRoadLaneType())
            {
                case RoadLaneType.Forward:
                    collider.GetComponent<TrafficCarController>().SetNewSplineRoadLaneNodes(_availableLanes[randomLaneIndex]);
                    break;

                case RoadLaneType.ChangeLane:
                    int randomChangeLaneRoll = Random.Range(0, 6);
                    if(randomChangeLaneRoll == 3)
                    {
                        collider.GetComponent<TrafficCarController>().SetNewSplineRoadLaneNodes(_availableLanes[randomLaneIndex]);
                    }
                    else
                    {
                        foreach(SplineRoadLane lane in _availableLanes)
                        {
                            if(lane.GetRoadLaneType() == RoadLaneType.Forward)
                            {
                                collider.GetComponent<TrafficCarController>().SetNewSplineRoadLaneNodes(lane);
                                break;
                            }
                        }
                    }
                    break;

                case RoadLaneType.Parking:
                    int randomParkingLaneRoll = Random.Range(0, 6);
                    if(randomParkingLaneRoll >= 1 && !collider.GetComponent<TrafficCarController>().GetRecentlyParked())
                    {
                        collider.GetComponent<TrafficCarController>().SetNewSplineRoadLaneNodes(_availableLanes[randomLaneIndex]);
                    }
                    else
                    {
                        foreach(SplineRoadLane lane in _availableLanes)
                        {
                            if(lane.GetRoadLaneType() == RoadLaneType.Forward)
                            {
                                collider.GetComponent<TrafficCarController>().SetNewSplineRoadLaneNodes(lane);
                                break;
                            }
                        }
                    }
                    break; 

                default:
                    break;
            }
        }
    }
}
