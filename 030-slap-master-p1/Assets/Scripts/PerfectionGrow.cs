using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PerfectionGrow : MonoBehaviour
{
    [SerializeField] float moveDuration=1f;
    [SerializeField] Ease moveEase = Ease.Linear;
    [SerializeField] float growingScale = 1f;
    // Start is called before the first frame update
    void OnEnable()
    {
        Vector3 firstScale = transform.localScale;
        Vector3 toUp = new Vector3(transform.localScale.x* growingScale,transform.localScale.y* growingScale, 1f);
        
        transform.DOScale(toUp, moveDuration).SetEase(moveEase)
            .OnComplete(() =>
            {
                // text.transform.localScale = Vector3.one;
                // transform.localScale = firstScale;
                transform.DOScale(Vector3.zero, moveDuration)
                .OnComplete(()=> 
                {
                    transform.localScale = firstScale;
                    this.gameObject.SetActive(false);
                    
                });
                
            });
    }


}
