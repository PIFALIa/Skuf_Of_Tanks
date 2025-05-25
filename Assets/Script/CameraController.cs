using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5.0f;
    [SerializeField] private float maxVerticalAngle = 80.0f;
    [SerializeField] private float minVerticalAngle = -80.0f;

    private float _rotationX = 0.0f;
    private float _rotationY = 0.0f;

    void Start()
    {
        // Блокируем курсор в центре экрана и делаем его невидимым
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Получаем начальные углы поворота камеры
        Vector3 eulerAngles = transform.eulerAngles;
        _rotationX = eulerAngles.y;
        _rotationY = -eulerAngles.x; // Инвертируем для правильной работы вертикального угла
    }

    void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        // Получаем ввод с мыши
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Обновляем углы поворота
        _rotationX += mouseX * rotationSpeed;
        _rotationY -= mouseY * rotationSpeed;

        // Ограничиваем вертикальный угол
        _rotationY = ClampAngle(_rotationY, minVerticalAngle, maxVerticalAngle);

        // Применяем вращение к камере
        transform.rotation = Quaternion.Euler(_rotationY, _rotationX, 0);
    }

    // Функция для ограничения углов поворота
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}