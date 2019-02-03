using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    Vector3 offset;
    public Transform target;

	void Start()
	{
        offset = target.position - transform.position;
	}
	

    void Update()
    {
        transform.position = target.transform.position - offset;
    }
}
