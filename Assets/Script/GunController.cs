using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform; // Ссылка на Transform камеры
    [SerializeField] private float _rotationSpeed = 10.0f; // Скорость следования пушки за камерой
    [SerializeField] private float _maxRotationSpeed = 360.0f; //Максимальная скорость вращения
    [SerializeField] private float _maxVerticalAngle = 80.0f;  // Максимальные углы поворота (вертикальные)
    [SerializeField] private float _minVerticalAngle = -80.0f;

    private Quaternion _targetRotation; // Целевой поворот, за которым нужно следовать
    private float _currentRotationSpeed; // Текущая скорость вращения, чтобы сгладить изменение скорости

    void Start()
    {
        // Проверяем, что камера установлена
        if (_cameraTransform == null)
        {
            Debug.LogError("Camera Transform is not assigned to GunController!");
            enabled = false; // Отключаем скрипт, чтобы избежать ошибок
            return;
        }

        // Инициализируем целевой поворот текущим поворотом камеры
        _targetRotation = _cameraTransform.rotation;
    }

    void Update()
    {
        FollowCamera();
    }

    private void FollowCamera()
    {
        // Получаем поворот камеры
        _targetRotation = _cameraTransform.rotation;
        //Ограничиваем вертикальный угол поворота
        float angleX = _targetRotation.eulerAngles.x;
        float angleY = _targetRotation.eulerAngles.y;

        angleX = ClampAngle(angleX, _minVerticalAngle, _maxVerticalAngle);
        // Создаем Quaternion с ограниченным углом X и оригинальным углом Y
        _targetRotation = Quaternion.Euler(angleX, angleY, 0);
        // Плавно поворачиваем пушку к целевому повороту
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