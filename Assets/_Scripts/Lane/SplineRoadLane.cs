using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Splines;

[ExecuteInEditMode()]
public class SplineRoadLane : MonoBehaviour
{
    [SerializeField] private SplineContainer _splineContainer;
    [SerializeField] private float _resolution;
    
    private List<Vector3> _laneNodesPositions;

    private float3 position;
    private float3 forward;
    private float3 upVector;

    private float3 nodePosition;

    void Start()
    {
        
    }

    private void OnValidate() 
    {
        CreateLaneNodes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateLaneNodes()
    {
        _laneNodesPositions = new List<Vector3>();

        float step = 1f / _resolution;

        for(int i = 0; i <= _resolution; i++)
        {
            float t = step * i;
            SampleRoadLane(t);
            _laneNodesPositions.Add(nodePosition);
        }
    }

    private void SampleRoadLane(float t)
    {
        _splineContainer.Evaluate(0, t, out position, out forward, out upVector);

        nodePosition = (Vector3)position;
    }

    private void OnDrawGizmos() 
    {
        //Handles.matrix = transform.localToWorldMatrix;

        foreach(Vector3 nodePosition in _laneNodesPositions)
        {
            Handles.SphereHandleCap(0, nodePosition, Quaternion.identity, 1f, EventType.Repaint);
            
        }
    }

    public List<Vector3> GetLaneNodes()
    {
        return _laneNodesPositions;
    }
}
