using UnityEngine;

public class LookAtCameraZOnly : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (mainCamera == null)
            Debug.Log("О чорт о боже");
            return;

        // Получаем текущее вращение объекта
        Vector3 currentRotation = gameObject.transform.rotation.eulerAngles;

        // Поворачиваем объект к камере
        transform.LookAt(mainCamera.transform);

        // Фиксируем вращение только по оси Z, сохраняем X и Y
        Vector3 newRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, newRotation.z);
    }
}