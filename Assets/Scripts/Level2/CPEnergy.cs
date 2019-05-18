using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPEnergy : MonoBehaviour
{
    bool dead = false;
    Slider slider;

    private void Start()
    {
        slider = GameObject.Find("Slider").GetComponent<Slider>();
    }

    public void LoseLife(float amount)
    {
        slider.value -= amount;

        if(slider.value <= 0f && !dead)
        {
            dead = true;
            //CP dies
            print("Captain Planet has died!");
        }
    }
}
