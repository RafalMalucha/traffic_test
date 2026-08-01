using UnityEngine;

public class CarDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider) 
    {
        if (collider.tag == "Traffic")
        {
            Destroy(collider.gameObject);
        }
    }
}
