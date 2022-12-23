using System;
using UnityEngine;

public class DoorDesign : MonoBehaviour
{
    [SerializeField] private int localX;
    [SerializeField] private int localY;

    public bool North;
    public bool East;
    public bool South;
    public bool West;
}