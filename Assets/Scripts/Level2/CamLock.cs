using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLock : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera cam;
    public GameObject[] boss;
    public GameObject audioManager;
    public AudioClip bossMusic;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioSource audio = audioManager.GetComponent<AudioSource>();
            StartCoroutine(StartBossFight(collision.gameObject));
            audio.clip = bossMusic;
            audio.Play();
            audio.volume = 0.65f;
        }
    }

    IEnumerator StartBossFight(GameObject player)
    {
        yield return new WaitForSeconds(.2f);

        GetComponent<BoxCollider2D>().isTrigger = false;
        cam.Follow = null;
        iTween.MoveTo(cam.gameObject, transform.position, 3f);

        foreach (GameObject bossGO in boss)
        {
            bossGO.SetActive(true);
        }

        if(player.transform.position.x < 45.8f)
        {
            iTween.MoveTo(player, new Vector3(45.8f, player.transform.position.y, 0), 1f);
        }

    }
}
