using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manual : MonoBehaviour
{
    public void GoBack()
    {
        CanvasManager.instance.CloseTopStackCanvas();
    }
}
