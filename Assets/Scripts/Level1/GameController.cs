using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public Sprite[] weaponIcons; //Weapon icons to display
    public GameObject weaponIcon; //Actual, current weapon icon displaying
    public TextMeshProUGUI score;
    public int currentScore = 0;
    //public GameController _controller;

	void Start()
	{
        currentScore = PlayerPrefs.GetInt("TotalScore");    //Get score from previous level
        UpdateWeaponIcon(0); //Show current weapon
        UpdateScore(0); //Show current score
	}
	
    public void UpdateWeaponIcon(int weapon)
    {
        ///0 = Earth
        ///1 = Fireball
        ///2 = Waterball
        ///3 = WindPower
        weaponIcon.GetComponent<Image>().sprite = weaponIcons[weapon];
    }

    public void UpdateScore(int amount)
    {
        currentScore += amount;                 //Increase score
        score.text = currentScore.ToString();   //Display new score
    }
}
