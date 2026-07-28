using UnityEngine;

public class TrafficCarController : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private TestCarController _testCarController;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _testCarController.SetThrottle(1f);
        _testCarController.SetSteeringInput(Random.Range(-1f, 1f));
    }
}
