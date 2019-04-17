using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Collider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyActivator parent = GetComponentInParent<EnemyActivator>();
        parent.IWasHit(gameObject, collision);
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
