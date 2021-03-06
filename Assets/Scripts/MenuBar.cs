using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BarType
{
    health,
    mana
}

public class MenuBar : MonoBehaviour
{
    Slider slider;
    public GameObject target;
    public BarType barType;

    void Start()
    {
        slider = GetComponent<Slider>();

        switch(barType)
        {
            case BarType.health:
                slider.maxValue = target.GetComponent<Health>().MaxHealthPoints;
                break;
            case BarType.mana:
                slider.maxValue = target.GetComponent<Mana>().MaxManaPoints;
                break;
        }
    }

    void Update()
    {
        switch (barType)
        {
            case BarType.health:
                slider.value = target.GetComponent<Health>().HealthPoints;
                break;
            case BarType.mana:
                slider.value = target.GetComponent<Mana>().ManaPoints;
                break;
        }
    }
}
