using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{

    [SerializeField] private Animator anim;
    [SerializeField] private string slapAnim;

    PlayerControl playerControl;

    public bool handClosingAnimStarted = false;
    public bool slapEnd;
    private void Awake()
    {
        playerControl = FindObjectOfType<PlayerControl>();
    }
    void SlapClosed()
    {
        anim.SetBool(slapAnim,false);
        handClosingAnimStarted = false;
        slapEnd = true;
        playerControl.ActiveMiss();
      
        
    }
    void SlapClosing()
    {
        slapEnd = false;
        handClosingAnimStarted = true;
    }
}
