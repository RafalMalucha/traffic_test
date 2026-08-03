using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class RandomMaterialPicker : MonoBehaviour
{
    [SerializeField] private Material[] _carMaterials;

    private void Awake()
    {
        GetComponent<MeshRenderer>().material = _carMaterials[Random.Range(0, _carMaterials.Length)];
    }
}
