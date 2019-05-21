using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour
{
    public GameObject explosion, ExtArm, claw;
    public BoxCollider2D playerCollider, clawCollider;
    public Sprite closedClaw, openClaw;
    public float extendSpeed, extendHeight;
    public int health = 5;
    public int damageToDeal = 1;
    public Vector2 lookDirection = Vector2.down;
    float originalHeight;
    bool sawPlayer;
    SpriteRenderer fist;
    float timeToPass;


    

    void Start()
    {
        fist = claw.GetComponent<SpriteRenderer>();
        originalHeight = ExtArm.transform.localPosition.y;
        //print(m_CurrentClipInfo[0].clip.name);

    }

    // Update is called once per frame
    void Update()
    {
            //Starting position (58.37, 86.344, 0)
            iTween.MoveTo(gameObject, 
                iTween.Hash("position", new Vector3(33f, transform.position.y), 
                "speed", 5f, 
                "easetype", iTween.EaseType.linear, 
                "looptype", iTween.LoopType.pingPong));

            RaycastHit2D hit = Physics2D.Raycast(transform.position, lookDirection, 10, LayerMask.GetMask("Player"));
            if (hit.collider != null && !sawPlayer)
            {
                sawPlayer = true;
                StartCoroutine(Extend());

            }
            if (hit.collider == null)
            {
                sawPlayer = false;
            }
    }

    IEnumerator Extend()
    {
        iTween.Pause(gameObject);
        iTween.MoveTo(ExtArm,
            iTween.Hash("position", new Vector3(ExtArm.transform.localPosition.x, extendHeight),
            "isLocal", true, 
            "time", extendSpeed,
            "easetype", iTween.EaseType.linear));
        yield return new WaitForSeconds(extendSpeed); //arm fully extended
        fist.sprite = closedClaw;
        timeToPass = .5f;
        StartCoroutine(Retract());
    }

    //Check for enemy in grasp && Retract
    IEnumerator Retract()
    {
        bool touchingPlayer = false;
        bool grabbedPlayer = false;
        //timeToPass is set to 1 in function that calls this coroutine, and is being deducted Time.deltaTime in Update()
        while(timeToPass > 0)   //Check if player was grabbed by extended claw
        {
           touchingPlayer = Physics2D.IsTouching(playerCollider, clawCollider);
            if (touchingPlayer && !grabbedPlayer)
            {
                grabbedPlayer = true;
                StartCoroutine(GrabbingPlayer(playerCollider.gameObject));

                break;
            }
            timeToPass -= Time.deltaTime;
            yield return null;
        }

            if (grabbedPlayer)
            {
                yield return new WaitForSeconds(1.3f); //length of GrabbingPlayer
            }

        fist.sprite = openClaw;
        iTween.MoveTo(ExtArm,
            iTween.Hash("position", new Vector3(ExtArm.transform.localPosition.x, originalHeight),
            "isLocal", true,
            "time", extendSpeed,
            "easetype", iTween.EaseType.linear));
        yield return new WaitForSeconds(extendSpeed); //Claw is fully retracted
        ResumeMoving();
    }

    IEnumerator GrabbingPlayer(GameObject player)
    {
        player.GetComponent<CPPlayer>().alive = false;
        iTween.MoveTo(player.gameObject, claw.transform.position, .3f);
        //player.transform.position = claw.transform.position;
        player.GetComponent<CPEnergy>().LoseLife(damageToDeal);
        yield return new WaitForSeconds(1);
        player.GetComponent<CPEnergy>().LoseLife(damageToDeal);
        yield return new WaitForSeconds(.3f);
        player.GetComponent<CPPlayer>().alive = true;

    }

    void ResumeMoving()
    {
        iTween.Resume(gameObject);
    }

    public void ReceiveDamage()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        health--;

        if(health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        GameObject.Find("GameController").GetComponent<GameController>().UpdateScore(500);
    }
}
