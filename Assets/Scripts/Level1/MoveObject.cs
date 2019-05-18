using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{

	void Start()
	{
	
	}

    public Vector3 moveDirection;

    void Update()
    {
        transform.position += moveDirection * Time.deltaTime;

        if(CompareTag("EnemyBase"))
        {
            if(transform.position.x <= 2.3f)
            {
                GetComponent<MoveObject>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
}
