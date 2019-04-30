using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    
    public float speed;
    public float earthForce = 5;
    public Vector2 earthForceDirection;
    public float waterForce = 5;
    public Vector2 waterForceDirection;
    public float windSpinSpeed;

    Rigidbody2D rb2d;
    public float destroyTimer = 3f;
    public GameObject explosion;

    public enum WeaponType { Earth, Water, Fire, Wind, Heart, Rocket }
    public WeaponType type;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        switch (type)
        {
            case WeaponType.Earth:
                Vector2 prb1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity;
                rb2d.AddForce(earthForceDirection + prb1, ForceMode2D.Impulse);
                break;
            case WeaponType.Water:
                Vector2 prb2 = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity;
                rb2d.AddForce(waterForceDirection + prb2, ForceMode2D.Impulse);
                break;
            case WeaponType.Fire:
                rb2d.velocity = transform.right * speed;
                break;
            case WeaponType.Wind:
                break;
            case WeaponType.Rocket:
                rb2d.velocity = transform.right * speed;
                break;
            case WeaponType.Heart:
                break;
            default:
                print("Unknown weapon shot");
                break;
        }

    }
    
    void Update()
    {
        if(type == WeaponType.Wind)
        {
            transform.Rotate(new Vector3(0,0,1) * windSpinSpeed);
        }

        destroyTimer -= Time.deltaTime;
        if(destroyTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (!collision.CompareTag("MainCamera") && collision.transform != transform.parent)
        {
            if (type == WeaponType.Rocket && collision.CompareTag("Enemy"))
                return;
            
            if (collision.CompareTag("Player"))
                collision.GetComponent<Player>().ReceiveDamage();
            else
            {
               Enemy comp = collision.GetComponent<Enemy>();
                if (comp)
                    comp.GetPoints();
                Destroy(collision.gameObject);
            }
            
            Instantiate(explosion, collision.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }          
    }
}
