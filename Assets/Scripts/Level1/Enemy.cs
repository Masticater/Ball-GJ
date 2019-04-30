using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int PointValue; //10
    public GameObject Projectile; //Rocket
    public float Speed; //5
    public bool Active; //False


    private GameController gc;

    private void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }

    public void GetPoints()
    {
        gc.UpdateScore(PointValue);
    }
}
