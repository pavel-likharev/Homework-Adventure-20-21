using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    [SerializeField] private bool _isVerticalCamera;

    public bool IsVerticalCamera => _isVerticalCamera;
}
