using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _rotationSpeed = 10.0f;
    [SerializeField] private float _maxRotationSpeed = 360f;
    [SerializeField] private float _maxVerticalAngle = 80.0f;  
    [SerializeField] private float _minVerticalAngle = -80.0f;

    private Quaternion _targetRotation; 
    private float _currentRotationSpeed; 

    void Start()
    {

        if (_cameraTransform == null)
        {
            Debug.LogError("Camera Transform is not assigned to GunController!");
            enabled = false; 
            return;
        }


        _targetRotation = _cameraTransform.rotation;
    }

    void Update()
    {
        FollowCamera();
    }

    private void FollowCamera()
    {

        _targetRotation = _cameraTransform.rotation;

        float angleX = _targetRotation.eulerAngles.x;
        float angleY = _targetRotation.eulerAngles.y;

        angleX = ClampAngle(angleX, _minVerticalAngle, _maxVerticalAngle);

        _targetRotation = Quaternion.Euler(angleX, angleY, 0);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, _rotationSpeed * Time.deltaTime);
    }
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}