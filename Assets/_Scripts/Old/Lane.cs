using UnityEngine;

public class Lane : MonoBehaviour
{
    [SerializeField] private Transform[] _lanePoints;

    public Transform[] GetLanePoints()
    {
        return _lanePoints;
    }
}
