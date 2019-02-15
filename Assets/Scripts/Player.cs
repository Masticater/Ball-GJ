using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb2d;

    float xMovement, yMovement;
    public float speed;
    public float yMaxLimit, yMinLimit, xMinLimit, xMaxLimit;
    private readonly int maxHealth = 2, maxLives = 3;
    public int curHealth, curLives;
    public GameObject explosion;

    void Start()
	{
        rb2d = GetComponent<Rigidbody2D>();
        curHealth = maxHealth;
        curLives = maxLives;
	}
	

    void Update()
    {

    }

    void FixedUpdate()
    {
        Movement();  
    }

    void Movement()
    {
        xMovement = Input.GetAxisRaw("Horizontal");
        yMovement = Input.GetAxisRaw("Vertical");
        rb2d.AddForce(new Vector2 (xMovement, yMovement).normalized * speed);

        Vector2 targetPos = new Vector2(Mathf.Clamp(transform.position.x, xMinLimit, xMaxLimit), Mathf.Clamp(transform.position.y, yMinLimit, yMaxLimit));
        transform.position = targetPos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            ReceiveDamage();
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }

    }

    public void ReceiveDamage()
    {
        curHealth--;
        if (curHealth <= 0)
            Die();
    }

    void Die()
    {
        print("Crash and Burn!");
    }
}
