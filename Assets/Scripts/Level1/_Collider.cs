using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Collider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Transform parent = transform.parent;
        if (parent.gameObject.CompareTag("MainCamera"))
        {
            EnemyActivator cameraColliders = parent.gameObject.GetComponent<EnemyActivator>();
            cameraColliders.IWasHit(gameObject, collision);
        }
        else
        {
            parent.gameObject.GetComponent<EnemyPlatform>().DestroyColumn(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Projectiles projectile = collision.GetComponent<Projectiles>();
        if (projectile != null)
        {
            if (projectile.type == Projectiles.WeaponType.Fire ||
                projectile.type == Projectiles.WeaponType.Earth)
                Destroy(collision.gameObject);
        }
    }
}
