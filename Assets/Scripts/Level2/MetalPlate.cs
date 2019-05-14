using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalPlate : MonoBehaviour
{
    int hits = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hits++;
        GetComponent<Animator>().SetInteger("Damage", hits);
    }
}
