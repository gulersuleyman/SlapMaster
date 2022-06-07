using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildSwitchFromStock : MonoBehaviour
{
    [SerializeField] private Transform targetParent ;
    
   // [SerializeField] private CharScript charScript;
    
   // [SerializeField] private Animator _animator;
    
    [SerializeField] private bool switchNow;
    
    [SerializeField] private int botSwitchTo;
    
    [SerializeField] private List<GameObject> Bots;
    [SerializeField] private bool takeList;
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        if (takeList)
        {
            takeList = false;
            Bots= getChildrenAsObject(transform,1);
            Debug.Log("LIST HAS TAKEN");

        }
        if (switchNow)
        {
            switchNow = false;

            if (botSwitchTo<0||Bots.Count<=botSwitchTo)
            {
                return;
            }
            foreach (var VARIABLE in Bots)
            {
                VARIABLE.SetActive(false);
                VARIABLE.transform.parent = transform;
            }
            GameObject TargetBot= Bots[botSwitchTo];
            TargetBot.transform.parent = targetParent;
            TargetBot.transform.localPosition = Vector3.zero;
            TargetBot.transform.SetSiblingIndex(0);
            TargetBot.SetActive(true);

            specialSettings();

        }
    }

    private void specialSettings()
    {
        //--------------------------------------------------------------------------------------
      //  RagdollStateControl targetStateControl = TargetBot.GetComponent<RagdollStateControl>();
     //   charScript.RagdollStateControler = targetStateControl;
        //   _animator.avatar=TargetBot.GetComponent<AvatarHolder>()._Avatar;
    }

    private List<Transform> getChildrenAsTransforms(List<Transform> Subjects , int NumberOfLayerToSearch)
    {
        NumberOfLayerToSearch--;
         List<Transform> firstDepths = new List<Transform>();
         foreach (var VARIABLE in Subjects)
         {
              
             firstDepths.AddRange(getFirstDepthOfChildren(VARIABLE));
         }

         if (NumberOfLayerToSearch>0)
         {
             firstDepths.AddRange( getChildrenAsTransforms(firstDepths, NumberOfLayerToSearch));
         }

         return firstDepths;


    }
    private List<Transform> getFirstDepthOfChildren( Transform  Subject )
    {
        Transform [] allChild = Subject.GetComponentsInChildren<Transform>();
        List<Transform> firstDepth = new List<Transform>();
        foreach (var VARIABLE2 in allChild)
        {
            if (VARIABLE2.parent==Subject)
            {
                firstDepth.Add(VARIABLE2);
            }
        }

        return firstDepth;

    }
    private List<GameObject> getChildrenAsObject(Transform Subject , int NumberOfLayerToSearch)
    {
        List<GameObject> result = new List<GameObject>();
        List<Transform> temp =  getChildrenAsTransforms(Subject, NumberOfLayerToSearch);
        foreach (var VARIABLE in temp)
        {
            result.Add(VARIABLE.gameObject);
        }

        return result;
    }
    private List<Transform> getChildrenAsTransforms( Transform   Subject , int NumberOfLayerToSearch)
    {
        List<Transform> input =  new List<Transform>();
        input.Add(Subject);
        return  getChildrenAsTransforms(input, NumberOfLayerToSearch);
    }
}
