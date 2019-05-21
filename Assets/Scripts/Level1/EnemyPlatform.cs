using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlatform : MonoBehaviour
{
    public GameObject leftDestroyed, rightDestroyed;

    public void DestroyColumn(Transform column) 
    {
        ///Find which side of the enemy's column was hit, and replace the enemy with
        ///the correct missing-column-enemy        
        if (column.name == "LeftColumn")
        {
            Instantiate(leftDestroyed, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
           Instantiate(rightDestroyed, transform.position, Quaternion.identity);
           Destroy(gameObject);
        }
    }
}
