using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanUp : MonoBehaviour
{
    public float countdown = 5; //Destroy projectiles and other miscellaneous objects after set time

    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0)
            Destroy(gameObject);
    }
}
