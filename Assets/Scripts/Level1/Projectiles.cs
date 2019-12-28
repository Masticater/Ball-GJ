using UnityEngine;

public class Projectiles : MonoBehaviour
{
    
    public float speed; //Speed at which projectiles move
    public float earthForce = 5; //Force at which a boulder is thrown
    public Vector2 earthForceDirection; //Direction the boulder will be thrown
    public float waterForce = 5; //Force at which a water blob will be thrown
    public Vector2 waterForceDirection; //Direction the water blob will be thrown
    public float windSpinSpeed; //How fast the mini-tornadoes spin around the player

    Rigidbody2D rb2d;
    public float destroyTimer = 3f; //How long each power lasts
    public GameObject explosion; //Explosion when hitting an enemy

    public enum WeaponType { Earth, Water, Fire, Wind, Heart, Rocket } //Each projectile type
    public WeaponType type;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        switch (type)
        {
            case WeaponType.Earth:
                Vector2 prb1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity;
                rb2d.AddForce(earthForceDirection + prb1, ForceMode2D.Impulse);
                break;
            case WeaponType.Water:
                Vector2 prb2 = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity;
                rb2d.AddForce(waterForceDirection + prb2, ForceMode2D.Impulse);
                break;
            case WeaponType.Fire:
                rb2d.velocity = transform.right * speed;
                break;
            case WeaponType.Wind:
                break;
            case WeaponType.Rocket:
                rb2d.velocity = transform.right * speed;
                break;
            case WeaponType.Heart:
                break;
            default:
                Debug.Log("Unknown weapon shot");
                break;
        }

    }
    
    void Update()
    {
        if(type == WeaponType.Wind)
        {
            transform.Rotate(new Vector3(0,0,1) * windSpinSpeed); //Spin the mini-tornadoes to stay upright 
        }

        destroyTimer -= Time.deltaTime;
        if(destroyTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //If the projectile is an enemy's projectile
        if (!collision.CompareTag("MainCamera")) //If I did not hit the camera's colliders
        {
            if (collision.CompareTag("Player"))
                collision.GetComponent<Player>().ReceiveDamage();
            //If projectile is Player's projectile
            else if(collision.CompareTag("Enemy"))
            {
               Enemy comp = collision.GetComponent<Enemy>();
                if (comp != null)
                    comp.GetPoints();
                Destroy(collision.gameObject);
            }
            //Hitting anything destroys projectile and explodes
            Instantiate(explosion, collision.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }          
    }
}
