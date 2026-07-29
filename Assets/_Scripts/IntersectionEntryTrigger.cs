using UnityEngine;

public class IntersectionEntryTrigger : MonoBehaviour
{
    [SerializeField] private Lane[] _lanes;
    
    private void OnTriggerEnter(Collider collider) 
    {
        if (collider.tag == "Traffic")
        {
            collider.GetComponent<TrafficCarController>().SetNewLane(_lanes[(int)Random.Range(0, _lanes.Length)]);
            collider.GetComponent<TestCarController>().SetNewMaxSpeed(3f);
        }
    }
}
