using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public AudioSource audioManager;
    public AudioClip windowCrack, windowBreak, winSound;
    public Transform shotSpawn;
    public GameObject bullet, cleanGlass, brokenGlass, bossGiveUp, metalArm1, metalArm2;
    public int windowHits = 5;
    int maxWindowHits;
    public float minAttackTime, maxAttackTime;
    float attackTime;
    bool fighting = true;

    void Start()
    {
        attackTime = Random.Range(minAttackTime, maxAttackTime);
        maxWindowHits = windowHits;
    }

    void Update()
    {
        if (fighting)
        {
            attackTime -= Time.deltaTime;
            if (attackTime <= 0)
            {
                Instantiate(bullet, shotSpawn.position, shotSpawn.rotation);
                attackTime = Random.Range(minAttackTime, maxAttackTime);
            }
        }
    }

    public void WindowHit(GameObject player)
    {
        windowHits--;
        audioManager.PlayOneShot(windowCrack);
        if (windowHits == maxWindowHits / 2)
        {
            brokenGlass.GetComponent<SpriteRenderer>().enabled = true;
            cleanGlass.SetActive(false);
        }
        if(windowHits <= 0)
        {
            fighting = false;
            bossGiveUp.SetActive(true);
            metalArm1.SetActive(false);
            metalArm2.SetActive(false);
            brokenGlass.GetComponent<SpriteRenderer>().enabled = false;
            player.GetComponentInParent<CPPlayer>().alive = false;
            player.GetComponentInParent<Animator>().SetBool("Win", true);

            StartCoroutine(Win());
            
        }
    }

    IEnumerator Win()
    {
        
        audioManager.volume = 1;
        audioManager.PlayOneShot(windowBreak);
        yield return new WaitForSeconds(1.853f);
        audioManager.Stop();
        audioManager.PlayOneShot(winSound);
        yield return new WaitForSeconds(11.1f);
        GameController gc = GameObject.Find("GameController").GetComponent<GameController>();
        gc.UpdateScore(1000);
        PlayerPrefs.SetInt("TotalScore", gc.currentScore);
        PlayerPrefs.SetInt("LastLevel", 0);
        UnityEngine.SceneManagement.SceneManager.LoadScene(6);
    }
}
