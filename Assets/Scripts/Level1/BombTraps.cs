using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTraps : MonoBehaviour
{
    public float speed = 5; //movement speed
    public float xDir, yDir; //movement direction

    void Update()
    {
        //Move the group to the left
        transform.position = new Vector2(transform.position.x + xDir * Time.deltaTime, transform.position.y + yDir * Time.deltaTime);
        //Spin the group
        transform.Rotate(new Vector3(0, 0, 1) * speed);

        foreach (Transform child in transform)
        {
            child.transform.Rotate(new Vector3(0, 0, -1) * speed); //Spin each bomb so it is upright
        }
    }
}
