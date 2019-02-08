using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    
    public float speed;
    Rigidbody2D rb2d;
    public float destroyTimer = 3f;
    public GameObject explosion;

    public enum WeaponType { Earth, Water, Fire, Wind, Rocket }
    public WeaponType type;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.right * speed;
    }
    
    void Update()
    {
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
            Instantiate(explosion, collision.transform.position, Quaternion.identity);
            Destroy(gameObject);
            print(collision.tag);
        }
    }
}
