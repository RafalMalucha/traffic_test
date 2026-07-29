using UnityEngine;

public class Lane : MonoBehaviour
{
    [SerializeField] private Transform[] _lanePoints;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform[] GetLanePoints()
    {
        return _lanePoints;
    }
}
