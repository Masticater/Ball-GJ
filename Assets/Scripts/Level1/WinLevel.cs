﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLevel : MonoBehaviour
{

    public GameObject player;

    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<Player>().enabled = false;
            StartCoroutine(MoveToWin());
            PlayerPrefs.SetInt("TotalScore", player.GetComponent<Weapons>().gameController.currentScore);
            PlayerPrefs.SetInt("LastLevel", 4);
        }
    }


    IEnumerator MoveToWin()
    {
        iTween.MoveTo(player, iTween.Hash("position", new Vector3(0, -1.45f, 0),
            "time", 2.6f, "easing", iTween.EaseType.easeInOutSine));
        yield return new WaitForSeconds(2.5f);
        
        SceneManager.LoadScene(3);
    }
}
