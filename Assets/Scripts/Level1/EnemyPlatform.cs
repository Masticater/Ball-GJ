using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlatform : MonoBehaviour
{
    public GameObject leftDestroyed, rightDestroyed;

    private void Start()
    {
        
    }

    public void DestroyColumn(Transform column)
    {
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
    //private void OnDestroy(Transform column)
    //{
    //    if (column.name == "LeftColumn")
    //    {
    //        print("Left Destroyed");
    //        Instantiate(leftDestroyed, transform.position, Quaternion.identity);

    //    }
    //    else
    //    {
    //        Instantiate(rightDestroyed, transform.position, Quaternion.identity);
    //        print("Right Destroyed");
    //    }

    //}
}
