using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeToMiddle : MonoBehaviour //TODO CHANGES BEEN MADE
{
    [SerializeField] private bool work;

    public Transform objA;
    public Transform objB;

    [SerializeField] private bool useX;
    [SerializeField] private bool useY;
    [SerializeField] private bool useZ;

    [Range(0, 100)]
    public float PositionBetween=50;
     
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (work)
        {
            var positionA = objA.position;
            Vector3 bPos = objB.position;
            if (!useX)
            {
                bPos.x = positionA.x;
            }
            if (!useY)
            {
                bPos.y = positionA.y;
            }
            if (!useZ)
            {
                bPos.z = positionA.z;
            }

            
            transform.position  = positionA  +(PositionBetween* (bPos  - positionA ) / 100);
        }
    }
}
