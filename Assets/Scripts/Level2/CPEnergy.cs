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
    SpriteRenderer body;
    CPPlayer player;
    Powers powers;

    private void Start()
    {
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        body = GetComponent<SpriteRenderer>();
        player = GetComponent<CPPlayer>();
        powers = GetComponent<Powers>();
    }

    public void LoseLife(float amount)
    {
        if (!powers.isPower)
        {
            slider.value -= amount;
            blinks = 4;
            if(slider.value <= 0f && !dead)
            {
                dead = true;
                player.alive = false;
                player.anim.SetBool("Dead", true);
                //CP dies
                print("Captain Planet has died!");
            }

            if (!flashing && !dead)
            {
                flashing = true;
                StartCoroutine(Blink());
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
}
