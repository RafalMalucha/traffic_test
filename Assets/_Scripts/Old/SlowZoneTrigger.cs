using UnityEngine;

public class SlowZoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Traffic"))
        {
            collider.GetComponent<TestCarController>().SetNewMaxSpeed(5f);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Traffic"))
        {
            collider.GetComponent<TestCarController>().SetNewMaxSpeed(10f);
        }
    }
}
