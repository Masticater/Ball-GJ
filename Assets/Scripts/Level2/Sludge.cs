using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sludge : MonoBehaviour
{
    public float damage;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CPPlayer player = collision.GetComponent<CPPlayer>();
            CPEnergy energy = collision.GetComponent<CPEnergy>();

            if(!player.isSlowed && !energy.powers)
            {
                player.isSlowed = true;
                player.moveSpeed /= 2;
                
            }
            energy.LoseLife(damage * Time.deltaTime);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CPPlayer player = collision.GetComponent<CPPlayer>();

            if (player.isSlowed)
            {
                player.moveSpeed = player._moveSpeed;
                player.isSlowed = false;
            }

        }
    }
}


