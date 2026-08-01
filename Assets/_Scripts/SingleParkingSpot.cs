using UnityEngine;
using System.Collections;

public class SingleParkingSpot : MonoBehaviour
{
    [SerializeField] GameObject _parkingSpot;
    private bool _isOccupied = false;
    private Vector3 _savedTrafficPosition;
    private Quaternion _savedTrafficRotation;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collider) 
    {
        if (collider.tag == "Traffic" && _isOccupied == false)
        {
            _isOccupied = true;
            collider.GetComponent<TrafficCarController>().enabled = false;
            collider.GetComponent<TestCarController>().enabled = false;

            _savedTrafficPosition = collider.transform.position;
            _savedTrafficRotation = collider.transform.rotation;

            collider.transform.position = _parkingSpot.transform.position + new Vector3(0, 1, 0);
            collider.transform.rotation = _parkingSpot.transform.rotation;
            StartCoroutine(TrafficCarInParkingSpace(collider));
        }    
    }

    IEnumerator TrafficCarInParkingSpace(Collider collider)
    {
        yield return new WaitForSeconds(Random.Range(5, 6));

        collider.transform.position = _savedTrafficPosition;
        collider.transform.rotation = _savedTrafficRotation;

        collider.GetComponent<TrafficCarController>().enabled = true;
        collider.GetComponent<TestCarController>().enabled = true;

        yield return new WaitForSeconds(2);

        _isOccupied = false;
    }
}
