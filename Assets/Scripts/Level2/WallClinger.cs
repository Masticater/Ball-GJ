using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallClinger : Enemy
{
    public float attackTime, minTime, maxTime;

    FieldOfView fov;
    protected override void Start()
    {
        base.Start();
        attackTime = Random.Range(minTime, maxTime);
        fov = GetComponent<FieldOfView>();
    }


    void Update()
    {
        if(fov.visibleTargets.Count > 0)
        {
            Vector3 lookDir = fov.visibleTargets[0].transform.position - transform.position;
            transform.right = lookDir;

            attackTime -= Time.deltaTime;
            if (attackTime <= 0)
            {
                attackTime = Random.Range(minTime, maxTime);
                Instantiate(projectile, transform.position, transform.rotation);
            }

        }
    }
}
