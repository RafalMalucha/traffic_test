using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntersectionTrafficLights : MonoBehaviour
{
    [SerializeField] SingleTrafficLight[] _trafficLightsAxis1;
    [SerializeField] SingleTrafficLight[] _trafficLightsAxis2;

    void Awake()
    {
        foreach(SingleTrafficLight stl in _trafficLightsAxis2)
        {
            stl.ChangeLight();
        }
    }

    void Start()
    {
        StartCoroutine(SwitchLightsCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SwitchLightsCoroutine()
    {
        while(true)
        {
            foreach(SingleTrafficLight stl2 in _trafficLightsAxis2)
            {
                stl2.ChangeLight();
            }   
            yield return new WaitForSeconds(1);
            foreach(SingleTrafficLight stl1 in _trafficLightsAxis1)
            {
                stl1.ChangeLight();
            }

            yield return new WaitForSeconds(3);
        }
    }
}
