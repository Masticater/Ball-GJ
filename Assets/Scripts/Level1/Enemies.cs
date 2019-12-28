using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : Enemy
{
    public float attackTime, minTime, maxTime;
    public Transform shotSpawn;

    Rigidbody2D rb2d;

	protected override void Start()
	{
        base.Start();
        rb2d = GetComponent<Rigidbody2D>();
	}
	

    void Update()
    {
        if (active) //If entered into play area
        {
            attackTime -= Time.deltaTime;
            if (attackTime <= 0)
            {
                attackTime = Random.Range(minTime, maxTime); //Reset counter to time in between these two times
                Instantiate(projectile, shotSpawn.transform.position, shotSpawn.rotation); //Shoot
            }
        }
        rb2d.velocity = transform.right * speed; //Move toward player and play area
    }
}
