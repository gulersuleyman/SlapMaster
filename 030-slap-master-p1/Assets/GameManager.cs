using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public bool gameStart;
   public bool success;
   public bool lose;

   
   #region Singleton

   public static GameManager Instance { get; private set; }

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
   }

   #endregion

   private bool oneTimeSuccess;
   private bool oneTimeLose;

    public int playerScore;
    public int enemyScore;

    public event System.Action<int> OnPlayerScoreChanged;
    public event System.Action<int> OnEnemyScoreChanged;
    private void FixedUpdate()
   {
      if (success)
      {
         if (oneTimeSuccess)
         {
            return;
         }
         oneTimeSuccess = true;
      }

      if (lose)
      {
         if (oneTimeLose)
         {
            return;
         }

         oneTimeLose = true;
      }
   }
    public void IncreasePlayerScore(int score)
    {
        playerScore += score;
        OnPlayerScoreChanged?.Invoke(playerScore);
    }
    public void IncreaseEnemyScore(int score)
    {
        enemyScore += score;
        OnEnemyScoreChanged?.Invoke(enemyScore);
    }
}
