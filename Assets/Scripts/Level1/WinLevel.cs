using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLevel : MonoBehaviour
{

    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<Player>().enabled = false; //Disable the players movement
            StartCoroutine(MoveToWin()); 
            PlayerPrefs.SetInt("TotalScore", player.GetComponent<Weapons>().gameController.currentScore); //Save current score to carry it over to next level
            PlayerPrefs.SetInt("LastLevel", 4); //Next level to load, in case player skips cutscene or dies in that level
        }
    }

    IEnumerator MoveToWin()
    {
        //move player smoothly to the landing platform
        iTween.MoveTo(player, iTween.Hash("position", new Vector3(0, -1.45f, 0),
            "time", 2.6f, "easing", iTween.EaseType.easeInOutSine));

        yield return new WaitForSeconds(2.5f);
        //Load transition scene
        SceneManager.LoadScene(3);
    }
}
