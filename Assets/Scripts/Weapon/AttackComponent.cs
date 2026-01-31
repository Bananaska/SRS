using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    [SerializeField] private ShootingController _shooting;
    [SerializeField] private InputReceiver _inputReceiver;

    private void Awake()
    {
        _inputReceiver.OnMouseClicked += Attack;
    }
    private void Attack()
    {
        _shooting.TryShoot();
    }
    private void OnDestroy()
    {
        _inputReceiver.OnMouseClicked -= Attack;

    }
}
