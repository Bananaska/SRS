using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeChanger : MonoBehaviour
{
    public void SetVolume(float value)
    {
        AudioListener.volume = value;
    }
}
