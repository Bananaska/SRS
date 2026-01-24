using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReceiver : MonoBehaviour
{
    public event Action OnMouseClicked;
    public event Action<Vector2> OnMouseMove;
    private Vector2 MouseMovement;

    private void Awake()
    {
        
    }

    private void Update()
    {
        
    }

}
