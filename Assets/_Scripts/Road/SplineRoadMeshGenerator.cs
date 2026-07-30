using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Splines;

[ExecuteInEditMode()]
public class SplineRoadMeshGenerator : MonoBehaviour
{
    [SerializeField] private SplineContainer _splineContainer;
    [SerializeField] int _splineIndex;
    [SerializeField] SplineRoadLane _splineRoadLane;

    [Header("Road Parameters")]
    [SerializeField] private float _roadWitdh;
    [SerializeField] private float _resolution;

    [Header("Mesh")]
    [SerializeField] private MeshFilter _meshFilter;
    
    private float3 position;
    private float3 forward;
    private float3 upVector;

    private float3 p1;
    private float3 p2;

    private List<Vector3> vertsP1;
    private List<Vector3> vertsP2;

    void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _splineRoadLane = GetComponent<SplineRoadLane>();
    }

    void Update()
    {

    }

    private void OnValidate() 
    {
        GetVerts();
        //_splineRoadLane.CreateLanes();
    }

    private void OnEnable() 
    {
        Spline.Changed += OnSplineChanged;
        GetVerts();
        //_splineRoadLane.CreateLanes();
    }

    private void OnDisable() 
    {
        Spline.Changed -= OnSplineChanged;
    }
    
    private void OnSplineChanged(Spline arg1, int arg2, SplineModification arg3)
    {
        GetVerts();
        //_splineRoadLane.CreateLanes();
    }

    private void OnDrawGizmos() 
    {
        Handles.matrix = transform.localToWorldMatrix;

        foreach(Vector3 p1 in vertsP1)
        {
            Handles.SphereHandleCap(0, p1, Quaternion.identity, 1f, EventType.Repaint);
        }
        foreach(Vector3 p2 in vertsP2)
        {
            Handles.SphereHandleCap(0, p2, Quaternion.identity, 1f, EventType.Repaint);
        }
    }

    private void SampleSplineWidth(float t)
    {
        _splineContainer.Evaluate(_splineIndex, t, out position, out forward, out upVector);

        float3 right = Vector3.Cross(forward, upVector).normalized;
        p1 = position + (right * _roadWitdh);
        p2 = position + (-right * _roadWitdh);
    }

    private void GetVerts()
    {
        vertsP1 = new List<Vector3>();
        vertsP2 = new List<Vector3>();

        float step = 1f / _resolution;

        for(int i = 0; i <= _resolution; i++)
        {
            float t = step * i;
            SampleSplineWidth(t);
            vertsP1.Add(p1);
            vertsP2.Add(p2);
        }

        BuildMesh();
    }

    private void BuildMesh()
    {
        Mesh mesh = new Mesh();
        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();
        int offset = 0;

        int lenght = vertsP2.Count;

        for(int i = 1; i <= lenght; i++)
        {
            Vector3 p1 = vertsP1[i - 1];
            Vector3 p2 = vertsP2[i - 1];
            Vector3 p3; 
            Vector3 p4;

            if(i == lenght)
            {
                p3 = vertsP1[0];
                p4 = vertsP2[0];
            }
            else
            {
                p3 = vertsP1[i];
                p4 = vertsP2[i];
            }

            offset = 4 * (i - 1);

            int t1 = offset + 0;
            int t2 = offset + 2;
            int t3 = offset + 3;

            int t4 = offset + 3;
            int t5 = offset + 1;
            int t6 = offset + 0;

            verts.AddRange(new List<Vector3> {p1, p2, p3, p4});
            tris.AddRange(new List<int> {t1, t2, t3, t4, t5, t6});
        }

        mesh.SetVertices(verts);
        mesh.SetTriangles(tris, 0);
        _meshFilter.mesh = mesh;
    }
}
