using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : People
{
    public int playerIndex;
    [Header("体力条")]
    public Image strengthBar;
    [Header("口渴条")]
    public Image thirstBar;

    [Header("当前得分")]
    public int currentScore=0;

    [Header("玩家得分Text")]
    public TMP_Text Play_ScoreText;

    private Coroutine dyingCor;
    [Header("濒死时间")]
    public float dyingDuration=10;


    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener(E_EventType.E_HitDrinkMachine,HitDrinkShop);
    }
    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener(E_EventType.E_HitDrinkMachine, HitDrinkShop);
    }

    void WinCheck()
    {
        if (currentScore >= 5000)
            PlayerWin();
    }

    void DyingCheck() 
    { 
        if(currentThristy<=0)
            PlayerThirstyDying();

        if (currentStrength <= 0)
            PlayerLost();
    }

    /// <summary>
    /// 如果玩家在濒死倒计时内口渴值重新高于20。则停止濒死协程
    /// </summary>
    void BackToLiveCheck() 
    {
        if (dyingCor != null && currentThristy >= 10)
        {
            StopCoroutine(dyingCor);
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    /// <summary>
    /// 自身回合内玩家将不会自动回复体力值
    /// </summary>
    public void InMyTurn(bool outRand) 
    {
        if (outRand)
            inMyTurn = true;
        else
            inMyTurn = false;
    }

  
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Drinkable>())
            IGetDrink(other.gameObject.GetComponent<Drinkable>());
    }

    /// <summary>
    /// 玩家通过得到分数获胜
    /// </summary>
    public void PlayerWin()
    {
        GameLogic.Instance.GetWinner(true,this);
    }
    public void PlayerLost() 
    {
        GameLogic.Instance.GetWinner(false, this);
    }

    /// <summary>
    /// 玩家因口渴濒死,维持10s后失败
    /// </summary>
    public void PlayerThirstyDying()
    {
        dyingCor = StartCoroutine(ThirstyDying());
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    IEnumerator ThirstyDying() 
    {
        Debug.Log("进入濒死状态");
        //播放濒死动画或提示
        yield return new WaitForSeconds(10);
        PlayerLost();
    }

    protected override void HitDrinkShop()
    {
        if (GameLogic.Instance.currentPlayerIndex == playerIndex)
        {
            base.HitDrinkShop();
            strengthBar.fillAmount = currentStrength/maxStrength;
        }//体力条更新
    }

    protected override void StrengthExpendUpdate()
    {
        base.StrengthExpendUpdate();
        //体力条更新
        strengthBar.fillAmount = currentStrength/maxStrength;
    }

    protected override void ThirstExpendUpdate()
    {
        base.ThirstExpendUpdate();
        //口渴条更新
        thirstBar.fillAmount=currentThristy/maxThristy;
    }

  
    /// <summary>
    /// 玩家获得分数
    /// </summary>
    /// <param name="score"></param>
    void IGetScore(int score) 
    {
        currentScore += score;
        Play_ScoreText.text = currentScore.ToString();
    }

    private void Update()
    {
        //体力自然增加
        StrengthExpendUpdate();
        //口渴值自然消耗
        ThirstExpendUpdate();

        WinCheck();
        DyingCheck();
        BackToLiveCheck();
    }

    public  void IGetDrink(Drinkable drink) 
    {
        IGetScore(drink.score);
        DrinkIt(drink);
        //喝饮料音效
    }
}
