using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;        //View Distance
    [Range(0,360)]
    public float viewAngle;         //Peripheral vision

    public LayerMask targetMask;    //Things to look for
    public LayerMask obstacleMask;  //Things that can't see past

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>(); //Public so other scripts can access targets

    private void Start()
    {
        StartCoroutine(FindTargetsWithDelay(.2f));
    }

    IEnumerator FindTargetsWithDelay(float delay)   //Repeat searching function
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear(); //Clear targets to not have duplicates
        Collider2D[] targetsInRadius2D = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);  //Find all colliders within range

        for (int i = 0; i < targetsInRadius2D.Length; i++) //Check to make sure character can see target within range
        {
            Transform target = targetsInRadius2D[i].transform; //Specific target
            Vector3 dirToTarget = (target.position - transform.position).normalized; //Direction to target

            if(Vector3.Angle(transform.right, dirToTarget) < viewAngle / 2) //Is target within periphs?
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if(!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask)) //Is there a obstacle between character & target?
                {
                    visibleTargets.Add(target);

                    //do things to/with target if needed
                }
            }
        }
    }

    //This Function is used for Editor handles in FieldofViewEditor
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0);
    }
}
