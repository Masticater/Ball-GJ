using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D rb2d;

    float xMovement, yMovement;
    Vector2 startPosition;
    public float speed;
    public float yMaxLimit, yMinLimit, xMinLimit, xMaxLimit;
    private readonly int maxHealth = 2, maxLives = 3;
    public int curHealth, curLives;
    public GameObject explosion;
    public Vector2 crashingDirection;
    public static bool dead = false;

    void Start()
	{
        rb2d = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        curHealth = maxHealth;
        curLives = maxLives;
        dead = false;
	}
	

    void Update()
    {
    }

    void FixedUpdate()
    {
        if(curHealth > 0)
        {
            Movement();  
        }

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
        //else  (blink player)
         //   IFrames();
    }


    void Die()
    {
        Invoke("ResetLevel", 3);
        dead = true;
        GetComponent<BoxCollider2D>().enabled = false;
        Vector3 crashing = new Vector3(transform.rotation.x, transform.rotation.y, -20);
        transform.rotation = Quaternion.Euler(crashing);

        foreach(Transform child in transform)
        {
            if (child.gameObject.activeSelf == false){
                child.gameObject.SetActive(true);
            }
        }

        rb2d.drag = 0;
        rb2d.velocity = new Vector3(0, 0, 0);
        rb2d.AddForce(crashingDirection, ForceMode2D.Impulse);


        //Vector2 targetPos = new Vector2(transform.position.x + crashingDirection.x, transform.position.y + crashingDirection.y);
        //transform.position = targetPos;

        //print("Crash and Burn!");
    }

    void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //dead = false;
        //rb2d.drag = 5;
        //transform.rotation = Quaternion.Euler(0, 0, 0);
        //transform.position = startPosition;
    }
}
