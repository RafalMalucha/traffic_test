using UnityEngine;

[ExecuteInEditMode]
public class SingleTrafficLight : MonoBehaviour
{
    [SerializeField] private bool _isRed = true;
    [SerializeField] private GameObject _redLightIndicator;
    [SerializeField] private GameObject _greenLightIndicator;
    [SerializeField] private GameObject _stopTrigger;

    // Update is called once per frame
    void Update()
    {
        if (_isRed)
        {
            HandleLightChangeToRed();
        }
        else
        {
            HandleLightChangeToGreen();
        }
    }

    private void HandleLightChangeToGreen()
    {
        _greenLightIndicator.SetActive(true);
        _redLightIndicator.SetActive(false);
        _stopTrigger.SetActive(false);
    }

    private void HandleLightChangeToRed()
    {
        _greenLightIndicator.SetActive(false);
        _redLightIndicator.SetActive(true);
        _stopTrigger.SetActive(true);
    }

    public void ChangeLight()
    {
        _isRed = !_isRed;
    }
}
