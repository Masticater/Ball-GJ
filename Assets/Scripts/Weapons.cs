using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapons : MonoBehaviour
{
    public float attackTimerReset;
    float attackTimer;

    public GameController gameController;
    public GameObject[] attack;
    public Transform shotSpawn;
    int currentAttack = 0;

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
            Shoot(attack[currentAttack]);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            currentAttack++;
            if (currentAttack > attack.Length - 1)
                currentAttack = 0;
            print("Current attack is: " + currentAttack);
            UpdatePlayerUI();
        }
    }

    void UpdatePlayerUI()
    {
        gameController.UpdateWeaponIcon(currentAttack);
    }

    void Shoot(GameObject bulletType)
    {
        Vector2 spawnPoint;
        if (bulletType == attack[3])
            spawnPoint = transform.position;
        else
            spawnPoint = shotSpawn.transform.position;
        GameObject shot = Instantiate(bulletType, spawnPoint, Quaternion.identity);
        if (bulletType != attack[3])
            shot.transform.SetParent(transform);

    }
}
