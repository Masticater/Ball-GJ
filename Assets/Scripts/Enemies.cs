using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    Enemy enemy;
    public float attackTime, minTime, maxTime;
    public Transform shotSpawn;

    Rigidbody2D rb2d;

	void Start()
	{
        enemy = GetComponent<Enemy>();
        if (!enemy)
            print("No enemy script");
        rb2d = GetComponent<Rigidbody2D>();
	}
	

    void Update()
    {
        if (enemy.Active)
        {
            attackTime -= Time.deltaTime;
            if (attackTime <= 0)
            {
                attackTime = Random.Range(minTime, maxTime);
                Instantiate(enemy.Projectile, shotSpawn.transform.position, shotSpawn.rotation);
            }
        }
        rb2d.velocity = transform.right * enemy.Speed;
    }
}
