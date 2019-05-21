using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers : MonoBehaviour
{
    public GameController gameController;
    Animator anim;
    bool isTransforming = false;
    public bool isPower = false;
    Coroutine TransformWater = null;
    Coroutine TransformRock = null;
    CPPunch punch;
    
    int currentPower = 0;

    private void Start()
    {
        anim = GetComponent<Animator>();
        punch = GetComponentInChildren<CPPunch>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isPower)
        {
            currentPower++;
            if (currentPower > gameController.weaponIcons.Length - 1)
                currentPower = 0;
            UpdatePlayerUI();
        }

        if ((Input.GetKeyDown(KeyCode.LeftShift) && !isTransforming) || 
            (Input.GetKeyDown(KeyCode.LeftShift) && !isPower))
        {
            if(currentPower == 0)
            {
                isTransforming = true;
                TransformRock = StartCoroutine(TransformToRock());
            }
            if (currentPower == 1)
            {
                isTransforming = true;
                TransformWater = StartCoroutine(TransformToWater());
            }

        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {            
            StartCoroutine(PowerReturn());
        }
    }

    IEnumerator TransformToRock()
    {
        anim.SetBool("RockTransform", true);
        yield return new WaitForSeconds(.75f);
        anim.SetBool("IsPower", true);
        anim.SetBool("RockTransform", false);
        isPower = true;
    }

    IEnumerator TransformToWater()
    {
        anim.SetBool("WaterTransform", true);
        yield return new WaitForSeconds(.75f);
        anim.SetBool("IsPower", true);
        anim.SetBool("WaterTransform", false);
        isPower = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPower)
        {
            punch.Punch(collision);
        }
    }

    IEnumerator PowerReturn()
    {
        if (currentPower == 0)
        {
            StopCoroutine(TransformRock);
        }
        if (currentPower == 1)
        {
            StopCoroutine(TransformWater);
        }

        anim.SetBool("IsPower", false);
        
        isTransforming = true;
        if (currentPower == 0)
            anim.SetBool("RockReturn", true);
        else
            anim.SetBool("WaterReturn", true);
        anim.SetBool("RockTransform", false);
        anim.SetBool("WaterTransform", false);
        yield return new WaitForSeconds(.75f);
        isTransforming = false;
        anim.SetBool("RockReturn", false);
        anim.SetBool("WaterReturn", false);
        isPower = false;
    }

    void UpdatePlayerUI()
    {
        gameController.UpdateWeaponIcon(currentPower);
    }
}
