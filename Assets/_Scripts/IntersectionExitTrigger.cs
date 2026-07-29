using UnityEngine;

public class IntersectionExitTrigger : MonoBehaviour
{
    [SerializeField] private Lane _lane;
    
    private void OnTriggerEnter(Collider collider) 
    {
        if (collider.tag == "Traffic")
        {
            collider.GetComponent<TrafficCarController>().SetNewLane(_lane);
            collider.GetComponent<TestCarController>().SetNewMaxSpeed(10f);
        }
    }
}
