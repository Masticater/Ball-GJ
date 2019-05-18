using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2Projectile : MonoBehaviour
{
    public float damage = 1f;
    public float speed = 1;
    Rigidbody2D rb;
    public bool bullet = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (bullet)
        {
            Vector2 shotDirection = new Vector2(transform.right.x, transform.right.y) * speed;
            rb.AddForce(shotDirection, ForceMode2D.Impulse);
        }
        else
        {
            rb.gravityScale = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<CPEnergy>().LoseLife(damage);
            Destroy(gameObject);
        }
        else if (!collision.CompareTag("Destructable") && !collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }

    }
}
