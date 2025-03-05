using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverPanel : BasePanel
{
    public TMP_Text winnerName;
    public TMP_Text winnerScore;
    public override void HidePanel()
    {
        base.HidePanel();
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
    }

    public override IEnumerator ShowPanelTweenEffect()
    {
      yield return  UITween.Instance.UIDoFade(transform,0,1,transTime);
    }

    protected override void Init()
    {
        base.Init();
    }
}
