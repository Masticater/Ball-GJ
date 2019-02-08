using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public float attackTime, minTime, maxTime;
    public Transform shotSpawn;
    public GameObject projectile;

    Rigidbody2D rb2d;
    public float speed = 5;

    public bool active = false;

	void Start()
	{
        rb2d = GetComponent<Rigidbody2D>();
	}
	

    void Update()
    {
        if (active)
        {
            attackTime -= Time.deltaTime;
            rb2d.velocity = transform.right * speed;
            if (attackTime <= 0)
            {
                attackTime = Random.Range(minTime, maxTime);
                GameObject shot = Instantiate(projectile, shotSpawn.transform.position, shotSpawn.rotation);
                shot.transform.SetParent(transform);
            }
        }
        else
        {
            rb2d.velocity = transform.right * speed;
        }
    }
}
