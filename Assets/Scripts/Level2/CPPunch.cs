using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPPunch : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Destructable"))
        {
            if(collision.name == "MetalPlate")
                collision.GetComponent<Destructable>().DamagePlate();
            else
            {
                collision.GetComponent<Destructable>().DestroyObject(collision.GetComponent<Destructable>().debris);
            }
        }
    }
}
