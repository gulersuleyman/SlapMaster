using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //anim
    [SerializeField] public Animator animator;
    [SerializeField] private string slapAnim;
    [SerializeField] private GameObject scoreIncreaseText;
    [SerializeField] private GameObject[] perfectionSprites;
    [SerializeField] private GameObject missText;
    [SerializeField] private float animSpeedIncreaser=1;

    public bool animOneTimeRun;
    public PlayerEvents playerEvents;

    float animSpeedAtCombo = 1;
    int perfectionIndex=0;
    bool isInCombo;
    bool canSmash = true;
    float canSmashTime = 0f;
    float canSmashTimeBoundary = 0.3f;
    bool didMissControlStarted;
   
    ComboControl comboControl;
    //ınput control
    [SerializeField] private bool mouseDown;
    
    #region Singleton

    public static PlayerControl Instance { get; private set; }

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
        playerEvents = FindObjectOfType<PlayerEvents>();
        comboControl = FindObjectOfType<ComboControl>();
    }

    #endregion

    private void Update()
    {
        IsInCombo();
        SmashTimeControl();
    }

    private void FixedUpdate()
    {
        
        if (!GameManager.Instance.lose && !GameManager.Instance.success)
        {
            MouseDownControl();
            if (mouseDown)
            {
                SlapRun();
            }
            
          
        }
        
        
    }


    void MouseDownControl()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
        {
            mouseDown = true;
        }
        else
        {
            mouseDown = false;
        }
            
        
       

        
    }

    public  void SlapRun()
    {
        //slap anim bitince false yapılacak
        animator.SetBool(slapAnim,true);
        animator.SetFloat("slapAnimSpeed", animSpeedAtCombo);
        didMissControlStarted = true;
    }
    
    void SmashTimeControl()
    {
        if (!canSmash)
        {
            canSmashTime += Time.deltaTime;
            if (canSmashTime > canSmashTimeBoundary)
            {
                canSmash = true;
                canSmashTime = 0f;
            }
        }
    }
    public void ActiveMiss()
    {
        if(didMissControlStarted && playerEvents.slapEnd )
        {
            animSpeedAtCombo = 1;
            missText.gameObject.SetActive(true);
            perfectionIndex = 0;
           // Debug.Log(missText.gameObject.transform.position.y);
        }
    }

    bool IsInCombo()
    {
        if(comboControl.countOfCombo>1)
        {
            isInCombo = false;
           perfectionIndex = 0;
            
        }
        else
        {
            isInCombo = true;
        }
        return isInCombo;
    }

    void ShowPerfectionSprites()
    {
        if(IsInCombo() && perfectionIndex>=3 && perfectionIndex<8 )
        {
            perfectionSprites[perfectionIndex -3].gameObject.SetActive(true);
            animSpeedAtCombo = animSpeedAtCombo * animSpeedIncreaser;
            animator.SetFloat("slapAnimSpeed", animSpeedAtCombo);
        }
       
    }
    /*private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.Instance.lose && !GameManager.Instance.success)
        {
            Debug.Log("contac to player head");
            if (other.gameObject.tag == "head")
            {
                switch (other.gameObject.GetComponent<HeadState>().slapType)
                {
                    case HeadState.SlapType.player:
                        DOTween.Kill("rotSpeed");
                        other.gameObject.GetComponent<HeadState>().Upgrade();
                        other.gameObject.GetComponent<HeadState>().SetRotateSpeed(3);

                        if (other.gameObject.GetComponent<HeadState>().dead)
                        {
                            Debug.Log("successs");
                            GameManager.Instance.success = true;
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
           // Debug.Log("contac to player head");
            if (other.gameObject.tag == "headplayer")
            {
                if (canSmash)
                {

                    DOTween.Kill("player");
                    

                    if (!playerEvents.handClosingAnimStarted)
                    {
                        other.gameObject.GetComponent<HeadState>().Upgrade();
                        other.gameObject.GetComponent<HeadState>().EffectRun();
                        other.gameObject.GetComponent<HeadState>().SetRotatePlayerSpeed(3f);
                        GameManager.Instance.IncreasePlayerScore(1);
                        scoreIncreaseText.gameObject.SetActive(true);
                        perfectionIndex++;
                        didMissControlStarted = false;
                        comboControl.countOfCombo = 0;

                        ShowPerfectionSprites();
                       // Debug.Log(perfectionIndex);

                    }
                    else
                    {
                        didMissControlStarted = false;
                        other.gameObject.GetComponent<HeadState>().SetOppositeRotatePlayerSpeed(-5f);
                        comboControl.countOfCombo++;
                        animSpeedAtCombo = 1;
                    }
                    canSmash = false;
                }

                if (other.gameObject.GetComponent<HeadState>().dead)
                        {
                            Debug.Log("successs");
                            //GameManager.Instance.success = true;
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
                        Debug.Log("successs");
                        other.gameObject.GetComponent<HeadState>().SetRotateSpeed(0);
                       
                        GameManager.Instance.success = true;
                    }
                    break;
            }

            
        }
    }*/
}
