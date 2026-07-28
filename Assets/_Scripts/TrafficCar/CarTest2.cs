using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class CarTest2 : MonoBehaviour
{

    [SerializeField] private NavMeshSurface _navSurface;
    [SerializeField] private NavMeshAgent _navAgent;
    [SerializeField] private GameObject _testTarget;

    void Start()
    {
        
    }


    void Update()
    {
        _navAgent.destination = _testTarget.transform.position;
    }
}
