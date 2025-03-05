using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : People
{
    public int playerIndex;
    [Header("������")]
    public Image strengthBar;
    [Header("�ڿ���")]
    public Image thirstBar;

    [Header("��ǰ�÷�")]
    public int currentScore=0;

    [Header("��ҵ÷�Text")]
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
    /// ���ͨ���õ�������ʤ
    /// </summary>
    public void PlayerWin()
    {
        GameLogic.Instance.GetWinner(1,this);
    }

    /// <summary>
    /// �����ڿ�ʧ��
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
        }//����������
    }

    protected override void StrengthExpendUpdate()
    {
        base.StrengthExpendUpdate();
        //����������
        strengthBar.fillAmount = currentStrength/maxStrength;
    }

    protected override void ThirstExpendUpdate()
    {
        base.ThirstExpendUpdate();
        //�ڿ�������
        thirstBar.fillAmount=currentThristy/maxThristy;
    }

  
    /// <summary>
    /// ��һ�÷���
    /// </summary>
    /// <param name="score"></param>
    void IGetScore(int score) 
    {
        currentScore += score;
        Play_ScoreText.text = currentScore.ToString();
    }

    private void Update()
    {
        //������Ȼ����
        StrengthExpendUpdate();
        //�ڿ�ֵ��Ȼ����
        ThirstExpendUpdate();

        WinCheck()
            ;
        LostCheck();
    }

    public  void IGetDrink(Drinkable drink) 
    {
        IGetScore(drink.score);
        DrinkIt(drink);
        //��������Ч
    }

  
}
