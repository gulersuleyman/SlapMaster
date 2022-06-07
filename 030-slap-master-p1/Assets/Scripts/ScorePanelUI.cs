using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ScorePanelUI : MonoBehaviour
{
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI enemyScoreText;
    


    [SerializeField] float moveDuration;
    

    [SerializeField] Ease moveEase = Ease.Linear;


    private void Start()
    {
        GameManager.Instance.OnPlayerScoreChanged += HandleOnPlayerScoreChanged;
        GameManager.Instance.OnEnemyScoreChanged += HandleOnEnemyScoreChanged;
        HandleOnPlayerScoreChanged();
        HandleOnEnemyScoreChanged();
        

    }
    

    private void OnDisable()
    {
        GameManager.Instance.OnPlayerScoreChanged -= HandleOnPlayerScoreChanged;
        GameManager.Instance.OnEnemyScoreChanged -= HandleOnEnemyScoreChanged;
    }
    private void HandleOnPlayerScoreChanged(int score=0)
    {
        playerScoreText.text = GameManager.Instance.playerScore.ToString();
        ScoreChangeAnimation(playerScoreText);
    }
    private void HandleOnEnemyScoreChanged(int score=0)
    {
        enemyScoreText.text = GameManager.Instance.enemyScore.ToString();
        ScoreChangeAnimation(enemyScoreText);
    }
    private void ScoreChangeAnimation(TextMeshProUGUI text)
    {
        Vector3 toScale = new Vector3(1.2f, 1.2f, 2);
        text.transform.eulerAngles = Vector3.zero;
        text.transform.DOScale(toScale, moveDuration).SetEase(moveEase)
            .OnComplete(()=> {
                // text.transform.localScale = Vector3.one;
                text.transform.DOScale(Vector3.one, moveDuration);
            });
    }
    
}
