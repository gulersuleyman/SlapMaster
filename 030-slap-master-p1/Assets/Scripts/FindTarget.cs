using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTarget : MonoBehaviour
{
    //find target
    [SerializeField] private float radius;
    [SerializeField] private LayerMask layer;
    [SerializeField] private Collider[] targetList;
    
    
    private void FixedUpdate()
    {
        if (!GameManager.Instance.lose && !GameManager.Instance.success)
        {
            FindTargetHead();
        }
       
    }

    void FindTargetHead()
    {
        targetList = Physics.OverlapSphere(transform.position, radius, layer);
        
        //target list de eleman varsa attack yoksa atttack iptal
        if (targetList.Length == 0)
        {
            EnemyControl.Instance.animOneTimeRun = false;
            return;
        }
        EnemyControl.Instance.SlapRun();
        
    }
    
    
        
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,radius);
    }
#endif
}
