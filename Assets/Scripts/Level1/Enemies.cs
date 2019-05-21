using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    Enemy enemy; //base of enemies
    public float attackTime, minTime, maxTime;
    public Transform shotSpawn;

    Rigidbody2D rb2d;

	void Start()
	{
        enemy = GetComponent<Enemy>();
        rb2d = GetComponent<Rigidbody2D>();
	}
	

    void Update()
    {
        if (enemy.Active) //If entered into play area
        {
            attackTime -= Time.deltaTime;
            if (attackTime <= 0)
            {
                attackTime = Random.Range(minTime, maxTime); //Reset counter to time in between these two times
                Instantiate(enemy.Projectile, shotSpawn.transform.position, shotSpawn.rotation); //Shoot
            }
        }
        rb2d.velocity = transform.right * enemy.Speed; //Move toward player and play area
    }
}
