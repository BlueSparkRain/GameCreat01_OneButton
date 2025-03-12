using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : BasePanel
{
    public TMP_Text winnerName;
    public TMP_Text winnerScore;

    public TMP_Text pressText;
    public bool gameOver;
    public override void HidePanel()
    {
        base.HidePanel();
        gameOver = false;
        pressText.gameObject.SetActive(false);
    }

    public override IEnumerator HidePanelTweenEffect()
    {
      yield return  UITween.Instance.UIDoFade(transform,1,0,transTime);
    }

    /// <summary>
    /// 同步胜利者游戏信息
    /// </summary>
    /// <param name="winner"></param>
    public void GetWinner(Player winner) 
    {
      winnerName.text = winner.playerName;
      winnerScore.text=winner.currentScore.ToString();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        MonoManager.Instance.StartCoroutine(WaitOver());
        MusicManager.Instance.PlaySound("弹出结算画面");
    }

    IEnumerator WaitOver() 
    {
        Debug.Log("woc");
        yield return new  WaitForSecondsRealtime(1);
        Debug.Log("HHH");
        gameOver = true;
        pressText.gameObject.SetActive(true);

    }

    public override IEnumerator ShowPanelTweenEffect()
    {
      yield return  UITween.Instance.UIDoFade(transform,0,1,transTime);

    }

    protected override void Init()
    {
        base.Init();
        Time.timeScale = 1.0f;
    }
    private void Update()
    {
        if (gameOver&&Input.GetKeyDown(KeyCode.Space))
        {
            UIManager.Instance.HidePanel<GameOverPanel>();
            SceneManager.LoadScene(0,LoadSceneMode.Single);
        }
    }

}
