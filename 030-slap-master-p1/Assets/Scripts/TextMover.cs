using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TextMover : MonoBehaviour
{
    [SerializeField] float moveDuration;
    [SerializeField] float upDistance=0.7f;
    [SerializeField] Ease moveEase = Ease.Linear;
    // Start is called before the first frame update
    void OnEnable()
    {
        Vector3 firstPos = transform.position;
        Vector3 toUp = new Vector3(transform.position.x,transform.position.y+ upDistance,transform.position.z);
        transform.eulerAngles = Vector3.zero;
        transform.DOMove(toUp, moveDuration).SetEase(moveEase)
            .OnComplete(() =>
            {
                // text.transform.localScale = Vector3.one;
                transform.position = firstPos;
                this.gameObject.SetActive(false);
            });
    }

   
}
