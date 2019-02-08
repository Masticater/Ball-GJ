using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public float attackTimerReset;
    float attackTimer;

    public GameObject attack;
    public Transform shotSpawn;

	void Start()
	{
        attackTimer = attackTimerReset;
	}
	

    void Update()
    {
        attackTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && attackTimer <= 0f)
        {
            attackTimer = attackTimerReset;
            Shoot(attack);
        }
    }

    void Shoot(GameObject bulletType)
    {
        GameObject shot = Instantiate(bulletType, shotSpawn.transform.position, Quaternion.identity);
        shot.transform.SetParent(transform);
    }
}
