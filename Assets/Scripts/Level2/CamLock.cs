using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLock : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera cam;
    public GameObject[] boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cam.Follow = gameObject.transform;
        GetComponent<BoxCollider2D>().isTrigger = false;

        foreach (GameObject bossGO in boss)
        {
            bossGO.SetActive(true);
        }
    }
}
