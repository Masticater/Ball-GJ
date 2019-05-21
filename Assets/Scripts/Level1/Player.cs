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

    private void Update()
    {
        xMovement = Input.GetAxisRaw("Horizontal");
        yMovement = Input.GetAxisRaw("Vertical");
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
        {
            Die();
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = false;
            blinks = 7;
            StartCoroutine(Blink());
        }
    }

    IEnumerator Blink()
    {
        ship.enabled = true;
        blinks--;
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
        curLives--;
        if (curLives > 0)
            Invoke("ResetLevel", 3);
        else
            StartCoroutine(GameOver());
        blinks = 7;
        dead = true;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponentInChildren<PolygonCollider2D>().enabled = false;
        Vector3 crashing = new Vector3(transform.rotation.x, transform.rotation.y, -20);
        transform.rotation = Quaternion.Euler(crashing);

        foreach(Animator anim in anims)
        {
            anim.SetBool("dead", true);
        }

        rb2d.drag = 0;
        rb2d.velocity = new Vector3(0, 0, 0);
        rb2d.AddForce(crashingDirection, ForceMode2D.Impulse);
    }

    IEnumerator GameOver()
    {
        PlayerPrefs.SetInt("LastLevel", SceneManager.GetActiveScene().buildIndex);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(5);
    }

    void ResetLevel()
    {
        StartCoroutine(Blink());
        transform.position = startPosition;
        dead = false;
        curHealth = maxHealth;
        rb2d.drag = 5;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        GetComponentInChildren<PolygonCollider2D>().enabled = true;


        foreach (Animator anim in anims)
        {
            anim.SetBool("dead", false);
        }
    }
}
