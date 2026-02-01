using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private InputReceiver _inputReceiver;
    [Header("Настройки чувствительности")]
    public float mouseSensitivity = 1f;

    [Header("Ограничения")]
    public float minYAngle = -90f;
    public float maxYAngle = 90f;

    [Header("Цели вращения")]
    public Transform playerBody; // Для горизонтального вращения тела игрока
    public Transform cameraTransform; // Для вертикального вращения камеры

    private float xRotation = 0f;

    private void Awake()
    {
        _inputReceiver.OnMouseMove += Rotate;

    }
    void Start()
    {
        // Скрываем и фиксируем курсор в центре экрана
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {

    }

    private void Rotate(Vector2 angle)
    {
        float mouseX = angle.x * mouseSensitivity * Time.deltaTime;
        float mouseY = angle.y * mouseSensitivity * Time.deltaTime;

        // Вертикальный поворот (вверх/вниз)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minYAngle, maxYAngle);

        // Применяем вращение камеры
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Горизонтальный поворот (влево/вправо) - вращаем тело игрока
        if (playerBody != null)
            playerBody.Rotate(Vector3.up * mouseX);
    }


}

