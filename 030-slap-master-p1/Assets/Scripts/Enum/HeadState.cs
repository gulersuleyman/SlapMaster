using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HeadState : MonoBehaviour
{
   public SlapType slapType;
   public RotateControl rotateControl;

   public GameObject[] levels;
   private int currentLevel = 0;
   private float _currentTime;

   public bool dead;

   public bool oneTimeDead;
   
   
   public float normalSpeed = 1.3f;

   
   //particleSystem
   public ParticleSystem effect;
   private void Start()
   {
      rotateControl.speedRotate = normalSpeed;
     // LevelSystem.Instance.DidYouNextLevelPanel = true;
   }
   private void FixedUpdate()
   {
      if (levels[levels.Length - 1].activeInHierarchy)
      {
         
         if (oneTimeDead)
         {
            return;
         }
         dead = true;
         oneTimeDead = true;
      }
   }

   public void EffectRun()
   {
      effect.Play();
   }
   public void Upgrade()
   {
      if (currentLevel < levels.Length - 1)
      {
         currentLevel++;
         SwitchObject(currentLevel-1);
            
        }
   }
   

   void SwitchObject(int lvl)
   {
      for (int i = 0; i < levels.Length; i++)
      {
         if (i == lvl)
         {
            levels[i].SetActive(true);
         }
         else
         {
            levels[i].SetActive(false);
         }
      }
   }
   
   public void SetRotatePlayerSpeed(float temp)
   {
      rotateControl.speedRotate = rotateControl.speedRotate + temp;
     Invoke("FirstSpeedChange",.1f);
   }
    public void SetOppositeRotatePlayerSpeed(float temp)
    {
        rotateControl.speedRotate = rotateControl.speedRotate + temp;
        Invoke("DirectionChange", .1f);
    }
    public void SetRotatePEnemypeed(float temp)
   {
      rotateControl.speedRotate = rotateControl.speedRotate + temp;
      Invoke("SpeedChange",.1f);
   }

   void FirstSpeedChange()
   {
      DOTween.To(()=> rotateControl.speedRotate, x=> rotateControl.speedRotate = x, normalSpeed, 5).SetId("player");
   }
    void DirectionChange()
    {
      DOTween.To(() => rotateControl.speedRotate, x => rotateControl.speedRotate = x, -normalSpeed, 5).SetId("player");
    }
   void SpeedChange()
   {
      DOTween.To(()=> rotateControl.speedRotate, x=> rotateControl.speedRotate = x, normalSpeed, 5).SetId("enemy");
   }
   
   
   public enum SlapType
   {
      player,enemy
   }


  
   
   
}
