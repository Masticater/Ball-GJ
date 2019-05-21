using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannon : MonoBehaviour
{
    Transform target;
	void Start()
	{
        target = GameObject.FindGameObjectWithTag("Player").transform; //Find the player
	}
	

    void Update()
    {
        transform.up = target.position - transform.position; //Aim at the player
    }
}
