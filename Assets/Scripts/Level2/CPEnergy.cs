using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPEnergy : MonoBehaviour
{
    bool dead = false;
    bool flashing, playingSound, flashBar;
    int blinks;
    [HideInInspector]
    public Slider slider;
    public Image fill;
    public float powerCost = 1.5f;
    SpriteRenderer body;
    CPPlayer player;
    [HideInInspector]
    public Powers powers;
    Coroutine blinking = null;

    private void Start()
    {
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        body = GetComponent<SpriteRenderer>();
        player = GetComponent<CPPlayer>();
        powers = GetComponent<Powers>();
    }

    private void Update()
    {
        if (powers.isPower)
        {
            slider.value -= powerCost * Time.deltaTime;

            if (slider.value <= 0 && !dead)
            {
                dead = true;
                StartCoroutine(Die());
            }
        }
    }

    public void LoseLife(float amount)
    {
        if (!powers.isPower)
        {

            slider.value -= amount;
            blinks = 4;
            
            if(slider.value <= 4 && !flashBar)
            {
                flashBar = true;
                StartCoroutine(FlashHealthBar(.3f));
            }

            if (slider.value <= 0f && !dead)
            {
                StartCoroutine(Die());
            }

            if (!flashing && !dead)
            {
                flashing = true;
                blinking = StartCoroutine(Blink());
            }

            if (!playingSound && !dead)
            {
                playingSound = true;
                StartCoroutine(Grunt());
            }
        }
    }

    IEnumerator Blink()
    {
        body.color = Color.white;
        blinks--;
        if (blinks == 0)
        {
            flashing = false;
            yield break;
        }
        yield return new WaitForSeconds(0.05f);
        body.color = Color.green;
        yield return new WaitForSeconds(0.05f);
        StartCoroutine(Blink());
    }

    IEnumerator Grunt()
    {
        AudioClip clip = player.Sounds[Random.Range(1, 4)]; //Grunt sounds
        player.audioSource.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        playingSound = false;
    }

    IEnumerator FlashHealthBar(float delay)   //Repeat searching function
    {
        while (slider.value <= 4)
        {
            fill.color = Color.red;
            yield return new WaitForSeconds(delay);
            fill.color = Color.yellow;
            yield return new WaitForSeconds(delay);
        }
        flashBar = false;
    }


    IEnumerator Die()
    {
        dead = true;
        player.alive = false;
        if(blinking != null)
            StopCoroutine(blinking);
        body.color = Color.white;
        player.anim.SetBool("Dead", true);
        player.audioSource.PlayOneShot(player.Sounds[4]);
        player.audioSource.PlayOneShot(player.Sounds[5]);
        yield return new WaitForSeconds(2.913f);
       
        yield return new WaitForSeconds(1);
        PlayerPrefs.SetInt("LastLevel", UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        UnityEngine.SceneManagement.SceneManager.LoadScene(5); //Load GameOver Scene
    }
}
