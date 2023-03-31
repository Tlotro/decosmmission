using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public static HPBar instance;
    public Slider slider;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        slider = GetComponent<Slider>();
    }

    static public void UpdateHP(int health)
    {
        if (instance != null)
            instance.slider.value = health;
    }
    static public void UpdateMaxHP(int maxhealth)
    {
        if (instance != null)
            instance.slider.maxValue = maxhealth;
    }
}
