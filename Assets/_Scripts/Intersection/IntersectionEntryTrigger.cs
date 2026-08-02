using UnityEngine;

public class IntersectionEntryTrigger : MonoBehaviour
{
    [SerializeField] private SplineRoadLane[] _lanes;
    
    private void OnTriggerEnter(Collider collider) 
    {
        if (collider.tag == "Traffic")
        {
            Debug.Log("enter intersection");
            collider.GetComponent<TrafficCarController>().SetNewSplineRoadLaneNodes(_lanes[(int)Random.Range(0, _lanes.Length)]);
            //collider.GetComponent<TestCarController>().SetNewMaxSpeed(3f);
        }
    }
}
