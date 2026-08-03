using UnityEngine;
using System.Collections;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _carPrefab;
    void Start()
    {
        StartCoroutine(SpawnCarsRandomInterval());
    }

    IEnumerator SpawnCarsRandomInterval()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(15, 45));
            GameObject newCar = Instantiate(_carPrefab, transform.position, transform.rotation);
        }
    }
}
