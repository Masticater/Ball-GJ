using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPPlayer : MonoBehaviour
{
    [HideInInspector]
    public Animator anim;
    Rigidbody2D rb;
    float horizontalMove;
    float verticalMove;

    public float moveSpeed;
    public float _moveSpeed { get; private set; }
    [HideInInspector]
    public bool isSlowed = false;
    public AudioClip[] Sounds;
    [HideInInspector]
    public AudioSource audioSource;

    bool punching;
    bool movingLeft = false;
    public bool alive = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        _moveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") ;
        verticalMove = Input.GetAxisRaw("Vertical");
        if(alive)
            UpdateAnim();
    }

    private void LateUpdate()
    {
        if(alive)
            Move();        
    }

    void UpdateAnim()
    {
        anim.SetFloat("xDir", Mathf.Abs(horizontalMove));

        if(horizontalMove < 0 && !movingLeft)
        {
            FlipSprite();
        }
        else if(horizontalMove > 0 && movingLeft)
        {
            FlipSprite();
        }

        anim.SetFloat("yDir", verticalMove);
        if (Input.GetKeyDown(KeyCode.Space) && !punching)
        {
            anim.SetBool("Punching", true);
            punching = true;
            StartCoroutine(ResetAnim());
            audioSource.PlayOneShot(Sounds[0]); //Punch sound
        }
    }

    IEnumerator ResetAnim()
    {
        float clipLength = .5f;
        yield return new WaitForSeconds(clipLength);
        anim.SetBool("Punching", false);
        punching = false;
    }

    void Move()
    {
        rb.velocity = new Vector2(horizontalMove, verticalMove).normalized * moveSpeed;
    }

    void FlipSprite()
    {
        movingLeft = !movingLeft;
        transform.Rotate(0, 180, 0);
    }
}
