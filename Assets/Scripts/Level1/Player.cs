using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D rb2d;

    float xMovement, yMovement;
    Vector2 startPosition; //Starting & ReSpawning location
    public float speed;
    public float yMaxLimit, yMinLimit, xMinLimit, xMaxLimit; //Player's bounds
    private readonly int maxHealth = 2, maxLives = 3;
    public int curHealth, curLives;
    public GameObject explosion; //Explosion on player when hit
    public Vector2 crashingDirection; //Direction to move when crashing
    public static bool dead = false;
    public SpriteRenderer ship; //Player's visibility
    private int blinks; //How many times to blink for i-frames after being hit
    Animator[] anims; //Small explosions on ship when crashing

    void Start()
	{
        anims = GetComponentsInChildren<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        curHealth = maxHealth;
        curLives = maxLives;
        dead = false;
	}

    private void Update() //Is player trying to move?
    {
        xMovement = Input.GetAxisRaw("Horizontal");
        yMovement = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate() 
    {
        if(curHealth > 0)//Has player died yet?
        {
            Movement();  //If not, allow the player to move
        }
    }

    void Movement()
    {
        
        rb2d.AddForce(new Vector2 (xMovement, yMovement).normalized * speed);

        Vector2 targetPos = new Vector2(Mathf.Clamp(transform.position.x, xMinLimit, xMaxLimit), 
            Mathf.Clamp(transform.position.y, yMinLimit, yMaxLimit)); //Is player trying to leave bounds?
        transform.position = targetPos; //If so, put them back into bounds
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Enemy")) //Did player hit an enemy with their ship?
        {
            ReceiveDamage(); //Deduct health
            Instantiate(explosion, transform.position, Quaternion.identity); //Explode
            Destroy(other.gameObject); //Kill enemy
        }

    }

    public void ReceiveDamage()
    {
        curHealth--; //1 point of damage
        if (curHealth <= 0) //Is player dead?
        {
            Die();
        }
        else
        {
            //If player is still alive, do not allow them to be hit by anything else for a given amount of time
            GetComponent<BoxCollider2D>().enabled = false; 
            blinks = 7; //Alternate visibility to show the player is invulnerable for now
            StartCoroutine(Blink());
        }
    }

    IEnumerator Blink()
    {
        ship.enabled = true; //Make ship visible
        blinks--;
        if (blinks == 0) //If i-Frames are finished
        {
            GetComponent<BoxCollider2D>().enabled = true; //Allow the player to be hit again
            yield break;
        }
        yield return new WaitForSeconds(0.1f);
        ship.enabled = false; //Make ship invisible
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Blink()); //Restart loop
        
    }

    
    void Die()
    {
        curLives--;
        if (curLives > 0) //Can the player respawn?
            Invoke("ResetLevel", 3); //If so, respawn in 3 seconds
        else
            StartCoroutine(GameOver()); //If not, end game
        blinks = 7; //Set blinks for respawn invinsibility
        dead = true; //Set to true so player is unable to move
        GetComponent<BoxCollider2D>().enabled = false; //Do not let the player be hit by anything else
        GetComponentInChildren<PolygonCollider2D>().enabled = false; //Let the player pass through solid objects
        Vector3 crashing = new Vector3(transform.rotation.x, transform.rotation.y, -20); //Direction to look when crashing
        transform.rotation = Quaternion.Euler(crashing); //Set look direction

        foreach(Animator anim in anims)
        {
            anim.SetBool("dead", true); //for each explosion, play animation
        }

        rb2d.drag = 0; //Player is frictionless, so it can move freely
        rb2d.velocity = new Vector3(0, 0, 0); //If player was moving, stop movement
        rb2d.AddForce(crashingDirection, ForceMode2D.Impulse); //Push the enemy to look like its crashing
    }

    IEnumerator GameOver()
    {
        PlayerPrefs.SetInt("LastLevel", SceneManager.GetActiveScene().buildIndex); //Make sure the level that will load after GameOver screen is this one
        yield return new WaitForSeconds(3); 
        SceneManager.LoadScene(5); //Load GameOver screen
    }

    void ResetLevel()
    {
        StartCoroutine(Blink()); //Start i-Frames
        transform.position = startPosition; //Place the player back at starting position
        dead = false; //Player is definitely not dead
        curHealth = maxHealth; //Player is in good health
        rb2d.drag = 5; //Reset drag so player can stop moving when they want to
        transform.rotation = Quaternion.Euler(0, 0, 0); //Straighten out the player so they don't look like they're drunk driving
        GetComponentInChildren<PolygonCollider2D>().enabled = true; //The player can bump into solid objects again

        foreach (Animator anim in anims)
        {
            anim.SetBool("dead", false); //Turn each explosion on the player off
        }
    }
}
