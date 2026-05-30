using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelterPoint : MonoBehaviour
{
    private bool _shelterIsFree;

    public void SetFree(bool isFree)
    {
        _shelterIsFree = isFree;
    }

    public bool GetFree()
    {
        return _shelterIsFree;
    }
}
