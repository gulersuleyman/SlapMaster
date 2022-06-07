using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    //anim
    [SerializeField] public Animator animator;
    [SerializeField] private string slapAnim;
    [SerializeField] private GameObject scorIncreaseText;
    [SerializeField] private bool enemySpeedCanIncrease;
    [SerializeField] private float animSpeedIncreaser = 1;

    EnemyEvents enemyEvents;


    public bool animOneTimeRun;

    float animSpeedAtCombo = 1;
    bool begin=false;
    #region Singleton

    public static EnemyControl Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("EXTRA : " + this + "  SCRIPT DETECTED RELATED GAME OBJ DESTROYED");
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        enemyEvents = FindObjectOfType<EnemyEvents>();
    }

    #endregion
    
    
    public  void SlapRun()
    {
        if (animOneTimeRun)
        {
            return;
        }
        //slap anim bitince false yapılacak
        if (begin)
        {
           // Debug.Log(animSpeedAtCombo);
            animator.SetBool(slapAnim, true);
            animOneTimeRun = true;
        }
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            begin = true;
        }
    }
    void AnimSpeedIncreaser()
    {
        if(enemySpeedCanIncrease)
        {
            SlapAnimSpeedControl();
            animSpeedAtCombo = animSpeedAtCombo + animSpeedIncreaser;
            animator.SetFloat("slapAnimSpeed", animSpeedAtCombo);
            
        }
        else
        {
            return;
        }
    }
    void SlapAnimSpeedControl()
    {
        if(enemyEvents.slapEnd)
        {
            animSpeedAtCombo = 1;
            enemyEvents.slapEnd = false;
        }

    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.Instance.lose && !GameManager.Instance.success)
        {
            Debug.Log("contac to");
            if (other.gameObject.tag == "head")
            {
                switch (other.gameObject.GetComponent<HeadState>().slapType)
                {
                    case HeadState.SlapType.enemy:
                        DOTween.Kill("rotSpeed");
                        other.gameObject.GetComponent<HeadState>().Upgrade();
                        other.gameObject.GetComponent<HeadState>().SetRotateSpeed(3);
                        if (other.gameObject.GetComponent<HeadState>().dead)
                        {
                            Debug.Log("loseeee");
                            GameManager.Instance.lose = true;
                        }
                        break;
                }

                
            }
        }
    }*/

    private void OnCollisionEnter(Collision other)
    {
        if (!GameManager.Instance.lose && !GameManager.Instance.success)
        {
            //Debug.Log("contac to");
            if (other.gameObject.tag == "headenemy" && begin)
            {
              
                
                DOTween.Kill("enemy");
                        other.gameObject.GetComponent<HeadState>().SetRotatePEnemypeed(3);
                        
                        other.gameObject.GetComponent<HeadState>().Upgrade();
                        other.gameObject.GetComponent<HeadState>().EffectRun();
                        scorIncreaseText.gameObject.SetActive(true);
                        GameManager.Instance.IncreaseEnemyScore(1);
                        AnimSpeedIncreaser();

                        if (other.gameObject.GetComponent<HeadState>().dead)
                        {
                            //Debug.Log("loseeee");
                           // GameManager.Instance.lose = true;
                        }
                

                
            }
        }
    }

    /*private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.tag == "head")
        {
            switch (other.gameObject.GetComponent<HeadState>().slapType)
            {
                case HeadState.SlapType.player:
                  
                    if (other.gameObject.GetComponent<HeadState>().dead)
                    { 
                       

                        other.gameObject.GetComponent<HeadState>().SetRotateSpeed(0);

                        Debug.Log("lose");
                        GameManager.Instance.lose = true;
                    }
                    break;
            }

            
        }
    }*/
}
