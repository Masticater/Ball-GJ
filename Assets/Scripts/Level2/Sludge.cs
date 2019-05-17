using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sludge : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CPPlayer player = collision.GetComponent<CPPlayer>();

            if(!player.isSlowed)
            {
                player.isSlowed = true;
                player.moveSpeed /= 2;
            }

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


