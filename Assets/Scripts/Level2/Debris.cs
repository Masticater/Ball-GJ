using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Debris : MonoBehaviour
{
    public float xForce, yForce, spinForce;
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-xForce, xForce), yForce),ForceMode2D.Impulse);
        GetComponent<Rigidbody2D>().AddTorque(spinForce,ForceMode2D.Impulse);
    }
    
}
