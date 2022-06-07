using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RotateControl : MonoBehaviour
{
    public float speedRotate = 1.5f;



    private void FixedUpdate()
    {
       // if (!GameManager.Instance.lose && !GameManager.Instance.success)

        {
            this.gameObject.transform.Rotate(0,speedRotate,0);

        }
        /*if (GameManager.Instance.lose || GameManager.Instance.success)
        {
            DOTween.To(()=> speedRotate, x=> speedRotate = x, 0, 10);
            this.gameObject.transform.Rotate(0,speedRotate,0);

        }*/
        
      
    }
}
