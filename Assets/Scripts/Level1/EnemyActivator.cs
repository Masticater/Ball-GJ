using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    public GameObject activater; //Tell enemies they're in play area
    public GameObject destroyer; //Destroy anything with a collider that leaves play area

    public void IWasHit(GameObject box, Collider2D collider)
    {
        if (box == activater) //Was the activator entered?
            ActivateEnemy(collider); //If so, activate enemy
        else
            DestroyEnemy(collider); //If not, destroy it, because it is leaving play area
    }

    void ActivateEnemy(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if(enemy != null) //if what entered play area was an enemy, activate it
        {
            enemy.Active = true;
        }
    }

    void DestroyEnemy(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) //Destroy anything but the player that tries to leave play area
            Destroy(collision.gameObject);
    }
}
