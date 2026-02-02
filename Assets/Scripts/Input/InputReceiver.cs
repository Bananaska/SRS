using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputReceiver : MonoBehaviour
{
    public event Action OnMouseClicked;
    public event Action<Vector2> OnMouseMove;
    private Vector2 _mouseMovement = new();


    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        _mouseMovement.x = mouseX;
        _mouseMovement.y = mouseY;

        if (_mouseMovement != Vector2.zero)
        {
            OnMouseMove?.Invoke(_mouseMovement);
        }
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseClicked?.Invoke();
        }
    }

}
