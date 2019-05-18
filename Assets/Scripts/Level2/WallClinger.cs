using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallClinger : MonoBehaviour
{
    public float attackTime, minTime, maxTime;

    Enemy enemy;
    FieldOfView fov;
    void Start()
    {
        enemy = GetComponent<Enemy>();
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
                Instantiate(enemy.Projectile, transform.position, transform.rotation);
            }

        }
    }
}
