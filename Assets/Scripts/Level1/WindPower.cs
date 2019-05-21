using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPower : MonoBehaviour
{
    public float speed = 5; //Speed the tornado will spin
    public Transform player;

	void Start()
	{
        player = GameObject.FindGameObjectWithTag("Player").transform; //Find the player, since it isn't a parent
	}
	

    void Update()
    {
        transform.position = player.transform.position; //Move group on top of player
        transform.Rotate(new Vector3(0,0,1) * speed); //

        if (transform.childCount == 0) //If all of the tornadoes have been destroyed, get rid of the empty parent, so the sound stops
            Destroy(gameObject);
    }
}
