using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{

    FieldOfView fov;
    float explodeTimer = 1;
    int blinks;
    SpriteRenderer body;
    bool flashing = false;
    public float blinkTimer = 1;
    public float xForce, yForce;

    public GameObject projectile;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        fov = GetComponent<FieldOfView>();
        body = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(fov.visibleTargets.Count > 0)
        {
            explodeTimer -= Time.deltaTime;
            if(explodeTimer <= 0 && !flashing)
            {
                flashing = true;
                blinks = 7;
                StartCoroutine(StartBlowUp());
            }
        }
    }

    IEnumerator StartBlowUp()
    {
        body.color = Color.white;
        blinks--;
        if (blinks == 0)
        {
            Explode();
            yield break;
        }
        yield return new WaitForSeconds(blinkTimer);
        body.color = Color.yellow;
        yield return new WaitForSeconds(blinkTimer);
        blinkTimer /= 2;
        StartCoroutine(StartBlowUp());
    }

    void Explode()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject sludge = Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 361)));
            sludge.GetComponent<L2Projectile>().bullet = false;
            sludge.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-xForce, xForce), yForce), ForceMode2D.Impulse);
        }

        gameObject.SetActive(false);
    }
}
