using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class CarTest : MonoBehaviour
{
    [SerializeField] private SplineRoadLane _splineRoadLane;
    [SerializeField] private SplineContainer _splineContainer;
    
    private List<Vector3> laneNodes;
    private float splineLenght;
    private float distancePercentage = 0f;
    
    void Start()
    {
        //StartCoroutine(WaitForLane());
        splineLenght = _splineContainer.CalculateLength();
    }

    IEnumerator WaitForLane()
    {
        yield return new WaitForSeconds(2);
        laneNodes = _splineRoadLane.GetLane1();
        if(laneNodes.Count > 0)
        {
            transform.position = laneNodes[0];
            StartCoroutine(MoveAlongCurrentLane());
        }
    } 

    IEnumerator MoveAlongCurrentLane()
    {
        foreach(Vector3 node in laneNodes)
        {
            yield return new WaitForSeconds(0.25f);
            transform.position = node;
        }

        yield return null;
    }

    void Update()
    {
        distancePercentage += 15.0f * Time.deltaTime / splineLenght;

        Vector3 currentPos = _splineContainer.EvaluatePosition(distancePercentage);
        transform.position = currentPos;

        if(distancePercentage > 1f)
        {
            distancePercentage = 0f;
        }

        Vector3 nextPos = _splineContainer.EvaluatePosition(distancePercentage + 0.1f);
        Vector3 dir = nextPos - currentPos;
        transform.rotation = Quaternion.LookRotation(dir, transform.up);
    }
}
