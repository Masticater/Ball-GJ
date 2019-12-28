using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int pointValue; //How much an enemy is worth when killing
    public GameObject projectile; //Bullet to be shot
    public float speed; //Move Speed
    public bool active; //Is in play area

    private GameController gc;

    protected virtual void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        if (gc == null)
        {
            print("No GameController Found");
        }
    }

    public void GetPoints() //add points when is killed
    {
        
        gc.UpdateScore(pointValue);
    }
}
