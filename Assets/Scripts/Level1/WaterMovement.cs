using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    // Used for the ocean under the player
    public float speed;
	
    void FixedUpdate()
    {
        Vector3 pos = transform.position;
        pos -= new Vector3(speed, 0) * Time.deltaTime; //Set new target position to the left of current position
        transform.position = pos; //Set water's new position
        
        if (transform.position.x <= -7f) //If water is off screen, set it back to the right of the play area
            transform.position = new Vector3 (7f, transform.position.y, transform.position.z);
    }
}
