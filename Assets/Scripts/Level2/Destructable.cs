using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    int hits = 0;
    public GameObject reward, debris;
    public int debrisAmount = 1;
    public AudioClip[] clips;
    public bool explodable;
    public GameObject explosion;
    GameObject audioManager;

    private void Start()
    {
        audioManager = GameObject.Find("SoundManager");
    }
    public void DamagePlate()
    {
        AudioClip clip = clips[Random.Range(0,clips.Length)];
        audioManager.GetComponent<AudioSource>().PlayOneShot(clip);

        hits++;
        GetComponent<Animator>().SetInteger("Damage", hits);

        if (hits > 2)
            DestroyObject(debris);
    }


    public void DestroyObject(GameObject _debris)
    {
        if(reward != null)
            Instantiate(reward, transform.position, Quaternion.identity);
        if(_debris != null)
        {
            for (int i = 0; i < debrisAmount; i++)
            {
                Instantiate(_debris, transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 361))));
            }
        }
        if (explodable)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }

        gameObject.SetActive(false);
    }
}