using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTraps : MonoBehaviour
{
    public float speed = 5;
    public float xDir, yDir;
    void Start()
	{
	}
	

    void Update()
    {

        transform.position = new Vector2(transform.position.x + xDir * Time.deltaTime, transform.position.y + yDir * Time.deltaTime);

        transform.Rotate(new Vector3(0, 0, 1) * speed);

        foreach (Transform child in transform)
        {
            child.transform.Rotate(new Vector3(0, 0, -1) * speed);
        }
    }
}
