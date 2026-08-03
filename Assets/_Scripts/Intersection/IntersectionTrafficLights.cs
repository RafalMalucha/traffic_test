using UnityEngine;
using System.Collections;

public class IntersectionTrafficLights : MonoBehaviour
{
    [SerializeField] SingleTrafficLight[] _trafficLightsAxis1;
    [SerializeField] SingleTrafficLight[] _trafficLightsAxis2;

    private readonly static WaitForSeconds _waitForSeconds = new WaitForSeconds(10);

    void Awake()
    {
        foreach (SingleTrafficLight stl in _trafficLightsAxis2)
        {
            stl.ChangeLight();
        }
    }

    void Start()
    {
        StartCoroutine(SwitchLightsCoroutine());
    }

    IEnumerator SwitchLightsCoroutine()
    {
        while (true)
        {
            foreach (SingleTrafficLight stl2 in _trafficLightsAxis2)
            {
                stl2.ChangeLight();
            }
            foreach (SingleTrafficLight stl1 in _trafficLightsAxis1)
            {
                stl1.ChangeLight();
            }

            yield return _waitForSeconds;
        }
    }
}
