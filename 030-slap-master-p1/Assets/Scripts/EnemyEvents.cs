using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEvents : MonoBehaviour
{

    [SerializeField] private Animator anim;
    [SerializeField] private string slapAnim;

    public bool slapEnd;
    public bool handClosingAnimStarted = false;
    void SlapClosed()
    {
        slapEnd = true;
        anim.SetBool(slapAnim, false);
        handClosingAnimStarted = false;

    }
    void SlapClosing()
    {
        slapEnd = false;
        handClosingAnimStarted = true;
    }

}
