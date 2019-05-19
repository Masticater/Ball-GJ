using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPEnergy : MonoBehaviour
{
    bool dead = false;
    bool flashing, playingSound;
    int blinks;
    [HideInInspector]
    public Slider slider;
    public float powerCost = 2f;
    SpriteRenderer body;
    CPPlayer player;
    Powers powers;
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
        }
    }

    public void LoseLife(float amount)
    {
        if (!powers.isPower)
        {

            slider.value -= amount;
            blinks = 4;
            
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

    IEnumerator Die()
    {
        dead = true;
        player.alive = false;
        StopCoroutine(blinking);
        body.color = Color.white;
        player.anim.SetBool("Dead", true);
        player.audioSource.PlayOneShot(player.Sounds[4]);
        player.audioSource.PlayOneShot(player.Sounds[5]);
        yield return new WaitForSeconds(2.913f);
       
        yield return new WaitForSeconds(1);
        UnityEngine.SceneManagement.SceneManager.LoadScene(5); //Load GameOver Scene
    }
}
