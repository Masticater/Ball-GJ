using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPPlayer : MonoBehaviour
{
    Animator anim;
    float horixontalMove;
    float moveSpeed = 1f;
    bool punching;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horixontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        UpdateAnim();
    }

    void UpdateAnim()
    {
        anim.SetFloat("xDir", Mathf.Abs(horixontalMove));

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
}
