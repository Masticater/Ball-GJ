using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Sprite[] weaponIcons;
    public GameObject weaponIcon;

	void Start()
	{
        UpdateWeaponIcon(0);
	}
	
    public void UpdateWeaponIcon(int weapon)
    {
        //0 = earth
        //1 = Fireball
        //2 = Waterball
        //3 = WindPower
        weaponIcon.GetComponent<Image>().sprite = weaponIcons[weapon];
        print("Changed Icon");
    }
}
