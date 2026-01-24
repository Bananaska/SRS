using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    [Header("Основные настройки")]
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 2f;
    public float sensitivityY = 2f;

    [Header("Ограничения угла")]
    public float minimumX = -360f;
    public float maximumX = 360f;
    public float minimumY = -90f;
    public float maximumY = 90f;

    [Header("Плавность")]
    public bool smooth = true;
    public float smoothTime = 5f;

    [Header("Компоненты")]
    public Transform character;
    public Transform cameraTransform;

    private float rotationX = 0f;
    private float rotationY = 0f;
    private Quaternion originalCharacterRotation;
    private Quaternion originalCameraRotation;

    void Start()
    {
        // Инициализация
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;

        if (character == null)
            character = transform.parent ?? transform;

        originalCharacterRotation = character.localRotation;
        originalCameraRotation = cameraTransform.localRotation;
    }

    void Update()
    {
        if (!Application.isFocused) return;

        if (axes == RotationAxes.MouseXAndY)
        {
            // Читаем ввод мыши
            float mouseX = Input.GetAxis("Mouse X") * sensitivityX;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivityY;

            // Применяем вращение
            rotationX += mouseX;
            rotationY += mouseY;

            // Ограничиваем углы
            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            rotationY = ClampAngle(rotationY, minimumY, maximumY);

            // Создаем кватернионы для вращения
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);

            // Применяем с плавностью или без
            if (smooth)
            {
                character.localRotation = Quaternion.Slerp(
                    character.localRotation,
                    originalCharacterRotation * xQuaternion,
                    smoothTime * Time.deltaTime
                );

                cameraTransform.localRotation = Quaternion.Slerp(
                    cameraTransform.localRotation,
                    originalCameraRotation * yQuaternion,
                    smoothTime * Time.deltaTime
                );
            }
            else
            {
                character.localRotation = originalCharacterRotation * xQuaternion;
                cameraTransform.localRotation = originalCameraRotation * yQuaternion;
            }
        }
        else if (axes == RotationAxes.MouseX)
        {
            // Только горизонтальное вращение
            float mouseX = Input.GetAxis("Mouse X") * sensitivityX;
            rotationX += mouseX;
            rotationX = ClampAngle(rotationX, minimumX, maximumX);

            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);

            if (smooth)
            {
                character.localRotation = Quaternion.Slerp(
                    character.localRotation,
                    originalCharacterRotation * xQuaternion,
                    smoothTime * Time.deltaTime
                );
            }
            else
            {
                character.localRotation = originalCharacterRotation * xQuaternion;
            }
        }
        else
        {
            // Только вертикальное вращение
            float mouseY = Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY += mouseY;
            rotationY = ClampAngle(rotationY, minimumY, maximumY);

            Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);

            if (smooth)
            {
                cameraTransform.localRotation = Quaternion.Slerp(
                    cameraTransform.localRotation,
                    originalCameraRotation * yQuaternion,
                    smoothTime * Time.deltaTime
                );
            }
            else
            {
                cameraTransform.localRotation = originalCameraRotation * yQuaternion;
            }
        }
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }

    // Методы для управления курсором
    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


}

