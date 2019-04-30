using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public Sprite[] weaponIcons;
    public GameObject weaponIcon;
    public TextMeshProUGUI score;
    public int currentScore = 0;
    //public GameController _controller;

	void Start()
	{
        UpdateWeaponIcon(0);
        UpdateScore(0);
	}
	
    public void UpdateWeaponIcon(int weapon)
    {
        //0 = earth
        //1 = Fireball
        //2 = Waterball
        //3 = WindPower
        weaponIcon.GetComponent<Image>().sprite = weaponIcons[weapon];
    }

    public void UpdateScore(int amount)
    {
        currentScore += amount;
        score.text = currentScore.ToString();
    }
}
