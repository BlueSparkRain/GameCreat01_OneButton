using System.Collections;
using TMPro;
using Unity.VisualScripting;
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

    [Header("�ڿ�����ֵText")]
    public TMP_Text thirstBarText;

    [Header("��������ֵText")]
    public TMP_Text strengthText;

    private Coroutine dyingCor;
    [Header("����ʱ��")]
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
    /// �������ڱ�������ʱ�ڿڿ�ֵ���¸���20����ֹͣ����Э��
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
    /// ����غ�����ҽ������Զ��ظ�����ֵ
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
    /// ���ͨ���õ�������ʤ
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
    /// �����ڿʱ���,ά��10s��ʧ��
    /// </summary>
    public void PlayerThirstyDying()
    {
        dyingCor = StartCoroutine(ThirstyDying());
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    IEnumerator ThirstyDying() 
    {
        Debug.Log("�������״̬");
        //���ű�����������ʾ
        yield return new WaitForSeconds(10);
        PlayerLost();
    }

    protected override void HitDrinkShop()
    {
        if (GameLogic.Instance.currentPlayerIndex == playerIndex)
        {
            base.HitDrinkShop();
            strengthBar.fillAmount = currentStrength/maxStrength;
            strengthText.text=currentStrength.ToString();
        }//����������
        Animator animator = GetComponentInChildren<Animator>();
        if (animator&&GameLogic.Instance.currentPlayerIndex==0)
        {
            animator.SetTrigger("Hit0");
        }
        if (animator && GameLogic.Instance.currentPlayerIndex == 1)
        {
            animator.SetTrigger("Hit1");
        }
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
        thirstBarText.text=currentThristy.ToString();
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

        WinCheck();
        DyingCheck();
        BackToLiveCheck();
    }

    public  void IGetDrink(Drinkable drink) 
    {
        IGetScore(drink.score);
        DrinkIt(drink);
        //��������Ч
    }
}
