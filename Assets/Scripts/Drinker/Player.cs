using TMPro;
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

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener(E_EventType.E_HitDrinkMachine,HitDrinkShop);
    }

    void WinCheck()
    {
        if (currentScore >= 5000)
            PlayerWin();
    }
    void LostCheck() 
    { 
        if(currentThristy<=0 ||currentStrength<=0)
            PlayerLost();
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener(E_EventType.E_HitDrinkMachine,HitDrinkShop);
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
        GameLogic.Instance.GetWinner(1,this);
    }

    /// <summary>
    /// 玩家因口渴失败
    /// </summary>
    public void PlayerLost()
    {
        GameLogic.Instance.GetWinner(0,this);
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
        //体力自然消耗
        StrengthExpendUpdate();
        //口渴值自然消耗
        ThirstExpendUpdate();

        WinCheck()
            ;
        LostCheck();
    }

    public  void IGetDrink(Drinkable drink) 
    {
        IGetScore(drink.score);
        DrinkIt(drink);
        //喝饮料音效
    }

  
}
