using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public float attackTimerReset; //Public, adjustable firing rate
    float attackTimer; //Firing rate

    public GameController gameController;
    public GameObject[] attack; //Objects to shoot
    public Transform shotSpawn; //Where to shoot from
    int currentAttack = 0;

	void Start()
	{
        attackTimer = attackTimerReset; //Initialize private firing rate
	}
	

    void Update()
    {
        if (!Player.dead) //If the player is alive, allow them to attack
        {
            attackTimer -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space) && attackTimer <= 0f)
            {
                attackTimer = attackTimerReset;
                //Find out what attack to shoot
                if (attack[currentAttack].GetComponent<Projectiles>().type == Projectiles.WeaponType.Wind)
                    attackTimer = 3; //how long the tornadoes will spin around you
                Shoot(attack[currentAttack]); //Shoot attack
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                currentAttack++; //iterate through the attacks
                if (currentAttack > attack.Length - 1) //if at the end of the list, start from the beginning
                    currentAttack = 0;
                UpdatePlayerUI(); //Show current equipped attack
            }
        }
    }

    void UpdatePlayerUI()
    {
        gameController.UpdateWeaponIcon(currentAttack);
    }

    void Shoot(GameObject bulletType)
    {
        Vector2 spawnPoint; //Depending on what attack is being shot, different spawn points are needed
        if (bulletType == attack[3]) //Wind attack is spawned on player
            spawnPoint = transform.position;
        else
            spawnPoint = shotSpawn.transform.position; //Everything else is shot out front
        GameObject shot = Instantiate(bulletType, spawnPoint, Quaternion.identity); //Shoot specific attack
        //if (bulletType != attack[3])
            //shot.transform.SetParent(transform); //Setting the parent so OnCollision doesn't damage player
    }
}
