using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDemo : MonoBehaviour
{
    [SerializeField] Timer timer1;
    [SerializeField] GameObject GameCanvas;
    [SerializeField] ParticleSystem endGameBlastPS, endGameFallingPS;


    PlayerControl playerControl;
    EnemyControl enemyControl;

    bool nextPanelOpened;
    bool returnPanelOpened;
    bool begin;
    int beginIndex = 0;
    bool particleSystemPlayed=false;
    private void Awake()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        enemyControl = FindObjectOfType<EnemyControl>();

        nextPanelOpened = false;
        returnPanelOpened = false;
        
        Time.timeScale = 1f;
        LevelSystem.Instance.DidYouReturnPanel = false;
        LevelSystem.Instance.DidYouNextLevelPanel = false;
    }
    private void Update()
    {
        TimeBegin();

        if(timer1.remainingDuration==0 && GameManager.Instance.playerScore>GameManager.Instance.enemyScore && !returnPanelOpened &&!begin)
        {

            Invoke("LateNextPanel", 3f);
            nextPanelOpened = true;
            GameCanvas.gameObject.SetActive(false);
            GameManager.Instance.success = true;
            FunAnimation(playerControl.animator, true);
            SadAnimation(enemyControl.animator, true);
            EndGamePS();
        }
        else if (timer1.remainingDuration == 0 && GameManager.Instance.playerScore <= GameManager.Instance.enemyScore && !nextPanelOpened && !begin)
        {
            Invoke("LateReturnPanel", 3f);
            returnPanelOpened = true;
            GameCanvas.gameObject.SetActive(false);
            GameManager.Instance.lose = true;
            FunAnimation(enemyControl.animator, true);
            SadAnimation(playerControl.animator, true);
            
        }
    }
    void TimeBegin()
    {
        if (beginIndex == 0)
        {
            begin = true;

            if (Input.GetMouseButtonDown(0) && begin)
            {
                timer1.SetDuration(30).Begin();
                begin = false;
                beginIndex++;
            }
        }
    }
    public void FunAnimation(Animator animator, bool isFun)
    {
        if (isFun == animator.GetBool("isFun")) return;

        animator.SetBool("isFun", isFun);
    }
    public void SadAnimation(Animator animator, bool isSad)
    {
        if (isSad == animator.GetBool("isSad")) return;

        animator.SetBool("isSad", isSad);
    }
    
   
    void LateNextPanel()
    {
        LevelSystem.Instance.DidYouNextLevelPanel = true;
    }
    void LateReturnPanel()
    {
        LevelSystem.Instance.DidYouReturnPanel = true;
    }
    void EndGamePS()
    {
        if (!particleSystemPlayed)
        {
            endGameBlastPS.Play();
            endGameFallingPS.Play();
            
            particleSystemPlayed = true;
        }
    }
}
