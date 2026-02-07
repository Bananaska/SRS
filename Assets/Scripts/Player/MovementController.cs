using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private InputReceiver _inputReceiver;

    [Header("Настройки чувствительности")]
    [SerializeField] private float _mouseSensitivity = 1f;

    [Header("Ограничения")]
    [SerializeField] private float _minYAngle = -90f;
    [SerializeField] private float _maxYAngle = 90f;
    [SerializeField] private float _minXAngle = -90f;
    [SerializeField] private float _maxXAngle = 90f;


    [Header("Цели вращения")]
    [SerializeField] private Transform playerBody; 
    [SerializeField] private Transform cameraTransform; 

    private float yRotation = 0f;
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

    private void Rotate(Vector2 angle)
    {
        float mouseX = angle.x * _mouseSensitivity * Time.deltaTime;
        float mouseY = angle.y * _mouseSensitivity * Time.deltaTime;

        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, _minYAngle, _maxYAngle);

        cameraTransform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);

        // Горизонтальный поворот (влево/вправо) - вращаем тело игрока
        if (playerBody != null)
        {
            xRotation -= mouseX;
            xRotation = Mathf.Clamp(xRotation, _minXAngle, _maxXAngle);
            playerBody.localRotation = Quaternion.Euler(0f, -xRotation, 0f);

        }
    }
}

