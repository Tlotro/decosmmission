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

    public void UpdateHP(int health)
    {
        slider.value = health;
    }
    public void UpdateMaxHP(int maxhealth)
    {
        slider.maxValue = maxhealth;
    }
}
