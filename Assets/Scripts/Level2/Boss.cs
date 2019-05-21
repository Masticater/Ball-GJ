using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public AudioSource audioManager; //Main audio source, to change audio/music
    public AudioClip windowCrack, windowBreak, winSound; //Sounds to play
    public Transform shotSpawn; //Position to shoot from
    public GameObject bullet, cleanGlass, brokenGlass, bossGiveUp, metalArm1, metalArm2; //Prefab objects of boss
    public int windowHits = 5; //public, adjustable Boss health
    int maxWindowHits; //Actual boss health
    public float minAttackTime, maxAttackTime; //Fire rate min/max
    float attackTime; //firerate countdown
    bool fighting = true; //Boss is alive

    void Start()
    {
        attackTime = Random.Range(minAttackTime, maxAttackTime);
        maxWindowHits = windowHits;
    }

    void Update()
    {
        if (fighting) //If boss is alive, shoot at player
        {
            attackTime -= Time.deltaTime;
            if (attackTime <= 0)
            {
                Instantiate(bullet, shotSpawn.position, shotSpawn.rotation);
                attackTime = Random.Range(minAttackTime, maxAttackTime);
            }
        }
    }

    public void WindowHit(GameObject player) //When player punches window
    {
        windowHits--;
        audioManager.PlayOneShot(windowCrack); 
        if (windowHits == maxWindowHits / 2) //if boss health is half gone, show cracked glass
        {
            brokenGlass.GetComponent<SpriteRenderer>().enabled = true;
            cleanGlass.SetActive(false);
        }
        if(windowHits <= 0) //If boss's health is depleated, disable glass and win level
        {
            fighting = false;
            bossGiveUp.SetActive(true);
            metalArm1.SetActive(false);
            metalArm2.SetActive(false);
            brokenGlass.GetComponent<SpriteRenderer>().enabled = false; //remove glass from existence
            player.GetComponentInParent<CPPlayer>().alive = false; //Disable player's movement
            player.GetComponentInParent<Animator>().SetBool("Win", true); //Play flex animation

            StartCoroutine(Win());
            
        }
    }

    IEnumerator Win()
    {
        audioManager.volume = 1; //Turn up volume, since this audio is quieter than the music
        audioManager.PlayOneShot(windowBreak); //Play the window shattering audio
        yield return new WaitForSeconds(1.853f);
        audioManager.Stop(); //Stop music
        audioManager.PlayOneShot(winSound); //Play the win audio
        yield return new WaitForSeconds(11.1f);
        GameController gc = GameObject.Find("GameController").GetComponent<GameController>();
        gc.UpdateScore(1000); //Add 1,000 points for beating the boss
        PlayerPrefs.SetInt("TotalScore", gc.currentScore);
        PlayerPrefs.SetInt("LastLevel", 0); //Set the load level to the main menu
        UnityEngine.SceneManagement.SceneManager.LoadScene(6); //Load win scene
    }
}
