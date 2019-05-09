using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPPlayer : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    float horizontalMove;
    float verticalMove;
    public float moveSpeed = 1f;
    bool punching;
    bool movingLeft = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") ;
        verticalMove = Input.GetAxisRaw("Vertical");
        UpdateAnim();
    }

    private void LateUpdate()
    {
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
