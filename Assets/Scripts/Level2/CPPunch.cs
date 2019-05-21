using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPPunch : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Punch(collision);
    }

    public void Punch(Collider2D collision)
    {
        if (collision.CompareTag("Destructable"))
        {
            if (collision.name == "MetalPlate")
                collision.GetComponent<Destructable>().DamagePlate();
            else if (collision.name == "BrokenGlass")
            {
                Boss boss = collision.GetComponentInParent<Boss>();
                boss.WindowHit(gameObject);
            }
            else if (collision.name == "Claw")
            {
                if (collision.isActiveAndEnabled)
                    collision.GetComponentInParent<Claw>().ReceiveDamage();
            }
            else
            {
                collision.GetComponent<Destructable>().DestroyObject(collision.GetComponent<Destructable>().debris);
            }
        }

    }
}
