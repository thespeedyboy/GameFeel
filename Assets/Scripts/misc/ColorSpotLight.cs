using UnityEngine;

public class ColorSpotLight : MonoBehaviour
{
    [SerializeField] private GameObject _spotLightHead;
    [SerializeField] private float _rotationSpeed = 20f;
    [SerializeField] private float _maxRotation = 45f;
    private float _currentRotation;

    private void Update()
    {
        RotateHead();
     
    }
    private void RotateHead()
    {
        _currentRotation += Time.deltaTime * _rotationSpeed;
        float z = Mathf.PingPong(_currentRotation, _maxRotation);
        _spotLightHead.transform.localRotation = Quaternion.Euler(0f, 0f,z);
    }
}
