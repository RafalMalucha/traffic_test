using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Splines;

[ExecuteInEditMode()]
public class SplineRoadLane : MonoBehaviour
{
    [SerializeField] private SplineContainer _splineContainer;
    [SerializeField] int _splineIndex;
    [SerializeField] private float _resolution;

    [Header("Test Car")]
    [SerializeField] private GameObject _testCar;

    private float3 position;
    private float3 forward;
    private float3 upVector;

    private float3 lane1;
    private float3 lane2;

    private List<Vector3> nodesLane1;
    private List<Vector3> nodesLane2;

    void Awake()
    {
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValidate() 
    {
        GetLanes();
    }

    public void CreateLanes()
    {
        GetLanes();
    }

    private void GetLanes()
    {
        nodesLane1 = new List<Vector3>();
        nodesLane2 = new List<Vector3>();

        float step = 1f / _resolution;

        for(int i = 0; i <= _resolution; i++)
        {
            float t = step * i;
            SampleRoadLane(t);
            nodesLane1.Add(lane1);
            nodesLane2.Add(lane2);
        }
    }

    private void OnDrawGizmos() 
    {
        Handles.matrix = transform.localToWorldMatrix;

        foreach(Vector3 node1 in nodesLane1)
        {
            Handles.SphereHandleCap(0, node1, Quaternion.identity, 0.25f, EventType.Repaint);
        }
        foreach(Vector3 node2 in nodesLane2)
        {
            Handles.SphereHandleCap(0, node2, Quaternion.identity, 0.25f, EventType.Repaint);
        }
    }

    private void SampleRoadLane(float t)
    {
        _splineContainer.Evaluate(_splineIndex, t, out position, out forward, out upVector);

        float3 right = Vector3.Cross(forward, upVector).normalized;
        lane1 = position + (right * 3f);
        lane2 = position + (-right * 3f);
    }

    public List<Vector3> GetLane1()
    {
        return nodesLane1;
    }
}
