using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    public GameObject activater;
    public GameObject destroyer;

    void Start()
	{

    }

    public void IWasHit(GameObject box, Collider2D collider)
    {
        if (box == activater)
            ActivateEnemy(collider);
        else
            DestroyEnemy(collider);
    }

    void ActivateEnemy(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.Active = true;
        }
    }

    void DestroyEnemy(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            Destroy(collision.gameObject);
    }
}
