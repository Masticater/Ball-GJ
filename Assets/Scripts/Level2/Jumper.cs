using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    public float damage = 1;
    public float jumpForce;
    bool detectedEnemy = false;
    bool falling = false;
    public float jumpTimerMin, jumpTimerMax;
    FieldOfView fov;
    Animator anim;
    Rigidbody2D rb;
    Vector2 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        fov = GetComponent<FieldOfView>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GetComponent<Rigidbody2D>();
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(fov.visibleTargets.Count > 0 && !detectedEnemy)
        {
            anim.SetBool("Landing", false);
            detectedEnemy = true;
            StartCoroutine(StartJumping());
        }

        if (falling)
        {
            if (transform.position.y <= startingPos.y)
            {
                transform.position = startingPos;
                rb.gravityScale = 0;
                rb.velocity = Vector3.zero;
                anim.SetBool("Landing", true);
                anim.SetBool("Jumping", false);
                detectedEnemy = false;
                falling = false;
            }

        }
    }

    IEnumerator StartJumping()
    {
        yield return new WaitForSeconds(Random.Range(jumpTimerMin, jumpTimerMax));

            anim.SetBool("Jumping", true);
            rb.gravityScale = 1;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        
        yield return new WaitForSeconds(.2f);
        falling = true;
    }
}
