using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    int hits = 0;
    public GameObject reward;
    public AudioClip[] clips;
    GameObject audioManager;
    private void Start()
    {
        audioManager = GameObject.Find("SoundManager");
    }
    public void DamagePlate()
    {
        AudioClip clip = clips[Random.Range(0, clips.Length)];
        audioManager.GetComponent<AudioSource>().PlayOneShot(clip);

        hits++;
        GetComponent<Animator>().SetInteger("Damage", hits);

        if (hits > 2)
            DestroyObject();
    }


    public void DestroyObject()
    {
        if(reward != null)
            Instantiate(reward, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}