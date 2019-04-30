using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPower : MonoBehaviour
{
    public float speed = 5;
    public Transform player;

	void Start()
	{
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	

    void Update()
    {
        transform.position = player.transform.position; 
        transform.Rotate(new Vector3(0,0,1) * speed);

        if (transform.childCount == 0)
            Destroy(gameObject);
    }
}
