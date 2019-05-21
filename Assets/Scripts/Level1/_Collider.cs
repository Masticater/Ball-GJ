using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Collider : MonoBehaviour
{

    /// Script used for both the Camera's Colliders (for enabling and destroying
    /// enemies), and the tower enemies. Both of which need to know which of the
    /// two colliders they have was hit 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Transform parent = transform.parent;
        if (parent.gameObject.CompareTag("MainCamera")) //Was the camera's colliders hit?
        {
            EnemyActivator cameraColliders = parent.gameObject.GetComponent<EnemyActivator>(); 
            cameraColliders.IWasHit(gameObject, collision);     //If so, find out which one
        }
        else //If not, then the tower enemy's collider was hit. Find out which one
        {
            parent.gameObject.GetComponent<EnemyPlatform>().DestroyColumn(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Projectiles projectile = collision.GetComponent<Projectiles>(); //Did a player bullet leave the play area?
        if (projectile != null)
        {
            if (projectile.type == Projectiles.WeaponType.Fire ||
                projectile.type == Projectiles.WeaponType.Earth)
                Destroy(collision.gameObject);  //If so, destroy it
        }
    }
}
