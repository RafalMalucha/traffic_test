using UnityEngine;

public class CarDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Traffic"))
        {
            Destroy(collider.gameObject);
        }
    }
}
