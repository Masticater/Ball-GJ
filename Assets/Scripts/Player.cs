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
    public SpriteRenderer ship;
    private int blinks;
    Animator[] anims;

    void Start()
	{
        anims = GetComponentsInChildren<Animator>();
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

    IEnumerator Blink()
    {
        ship.enabled = true;
        blinks--;
        print(blinks);
        if (blinks == 0)
        {
            GetComponent<BoxCollider2D>().enabled = true;
            yield break;
        }
        yield return new WaitForSeconds(0.1f);
        ship.enabled = false;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Blink());
        
    }

    

    void Die()
    {
        Invoke("ResetLevel", 3);
        blinks = 7;
        dead = true;
        GetComponent<BoxCollider2D>().enabled = false;
        Vector3 crashing = new Vector3(transform.rotation.x, transform.rotation.y, -20);
        transform.rotation = Quaternion.Euler(crashing);

        foreach(Animator anim in anims)
        {
            anim.SetBool("dead", true);
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
        StartCoroutine(Blink());
        transform.position = startPosition;
        dead = false;
        curHealth = maxHealth;
        rb2d.drag = 5;
        transform.rotation = Quaternion.Euler(0, 0, 0);


        foreach (Animator anim in anims)
        {
            anim.SetBool("dead", false);
        }

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);



    }
}
