using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MissionOrder : MonoBehaviour
{
    public abstract bool complete { get; }
    public abstract string showString { get; }
    public abstract void Setup();
}
