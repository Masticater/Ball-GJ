using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int PointValue; //How much an enemy is worth when killing
    public GameObject Projectile; //Bullet to be shot
    public float Speed; //Move Speed
    public bool Active; //Is in play area

    private GameController gc;

    private void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }

    public void GetPoints() //add points when is killed
    {
        gc.UpdateScore(PointValue);
    }
}
