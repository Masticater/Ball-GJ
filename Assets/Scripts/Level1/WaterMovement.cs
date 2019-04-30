using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    public float speed;

	void Start()
	{
	
	}
	
    void FixedUpdate()
    {
        Vector3 pos = transform.position;
        pos -= new Vector3(speed, 0) * Time.deltaTime;
        transform.position = pos;
        
        if (transform.position.x <= -7f)
            transform.position = new Vector3 (7f, transform.position.y, transform.position.z);
    }
}
