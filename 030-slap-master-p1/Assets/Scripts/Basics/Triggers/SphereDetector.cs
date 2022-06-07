using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDetector : MonoBehaviour
{
    public bool OnCollision;
    public bool onlyCurrentObject = true;
    [SerializeField] private List<Transform> DetectionPoints=new List<Transform>();
    [SerializeField] private List<float> detectionDistances=new List<float>();
    [SerializeField] private LayerMask detectionLayer;
    public Transform detectedObject;
    public List<Transform> detectedObjects=new List<Transform>();
    
    
    
    
    void Start()
    {
        if (onlyCurrentObject)
        {
            DetectionPoints.Add(transform);
            detectionDistances.Add(transform.lossyScale.x);
        }
        else
        {
            Transform [] children = GetComponentsInChildren<Transform>();

            foreach (var VARIABLE in children)
            {
                DetectionPoints.Add(VARIABLE);
                detectionDistances.Add(VARIABLE.lossyScale.x);
            }
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        OnCollision = false;

        int Finder=0;
        for (var index = 0; index < DetectionPoints.Count; index++)
        {
            var VARIABLE = DetectionPoints[index];
            OnCollision = Physics.CheckSphere(VARIABLE.position, detectionDistances[index], detectionLayer);
            if (OnCollision)
            {
                Finder = index;
                break;
            }
        }

        if (OnCollision)
        {
            List<Transform> temp = new List<Transform>();
            //-----------------------
            Collider[] hitColliders = Physics.OverlapSphere( DetectionPoints[Finder].position, detectionDistances[Finder]);
            Transform firstCollisionElement = hitColliders[0].transform; 
            foreach (var VARIABLE in hitColliders)
            {
                if ( detectionLayer == (detectionLayer | (1 << VARIABLE.gameObject.layer)) )
                {
                    var transform1 = VARIABLE.transform;
                    firstCollisionElement = transform1;
                    temp.Add(transform1);
                }
            }
            detectedObject = firstCollisionElement;
            detectedObjects = temp;
        }
    }
 
}
