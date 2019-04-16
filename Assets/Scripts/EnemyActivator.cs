using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]

public class EnemyActivator : MonoBehaviour
{
	void Start()
	{

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.Active = true;
           // print("Enabled enemy");
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
            Destroy(collision.gameObject);
    }
}
