using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();

    private void Start()
    {
        StartCoroutine(FindTargetsWithDelay(.2f));
    }
    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider2D[] targetsInRadius2D = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);
        //Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInRadius2D.Length; i++)
        {
            Transform target = targetsInRadius2D[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            if(Vector3.Angle(transform.right, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if(!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                    print("Saw player!!");
                    //do things to target
                }
            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }
        return new Vector3 (Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0);
    }
}
