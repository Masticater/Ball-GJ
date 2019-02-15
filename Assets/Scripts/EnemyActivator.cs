using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    public BoxCollider2D bc;

	void Start()
	{

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemies enemy = collision.GetComponent<Enemies>();
        if(enemy != null)
        {
            enemy.active = true;
           // print("Enabled enemy");
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
