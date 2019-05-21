using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    ParticleSystem ps;
    public int energyBonus;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<CPEnergy>().slider.value += energyBonus;
            ps.Stop();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<AudioSource>().Play();
        }
    }
}
