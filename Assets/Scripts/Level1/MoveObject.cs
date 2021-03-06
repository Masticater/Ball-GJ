﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Vector3 moveDirection;

    void LateUpdate()
    {
        transform.position += moveDirection * Time.deltaTime;

        if(CompareTag("EnemyBase"))
        {    ///Used specifically for the Enemy Base - The last moving object in the scene, so it stays in the play area
            if (transform.position.x <= 2.3f)
            {
                GetComponent<MoveObject>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
}
