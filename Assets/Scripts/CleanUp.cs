﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanUp : MonoBehaviour
{
    public float countdown = 5;

	void Start()
	{
	
	}
	

    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0)
            Destroy(gameObject);
    }
}
