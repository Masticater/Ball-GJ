using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : Exploder
{
    FieldOfView _fov;
    Rigidbody2D rb;
    public float speed = 2;

    override protected void Start()
    {
        base.Start();
        _fov = GetComponent<FieldOfView>();
        rb = GetComponent<Rigidbody2D>();
    }


    public override void Update()
    {
        base.Update();
        if(_fov.visibleTargets.Count > 0)
        {
            rb.velocity = (_fov.visibleTargets[0].position - transform.position).normalized * speed;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

}
