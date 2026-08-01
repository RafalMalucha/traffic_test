using UnityEngine;
using System.Collections;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _carPrefab;
    void Start()
    {
        StartCoroutine(SpawnCarsRandomInterval());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnCarsRandomInterval()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(5, 20));
            GameObject newCar = Instantiate(_carPrefab, transform.position, transform.rotation);
            newCar.GetComponent<TestCarController>().SetNewMaxSpeed(3f);
        }   
    }
}
