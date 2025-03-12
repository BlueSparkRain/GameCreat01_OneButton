using UnityEngine;

public class People : MonoBehaviour, ICanDrink
{
    public IDrink drink;

    #region �����ֶ�
    [Header("�������ֵ")]
    public float maxStrength = 100;
    [Header("��ǰ����ֵ")]
    public float currentStrength;

    [Header("�����û���������ֵ")]
    public float HitCost = 5;

    [Header("������λ��Ȼ����ֵ")]
    public float strengthExpendValue = 1 / 3;
    [Header("������Ȼ���ļ��")]
    public float strengthExpendInterval = 1;

    protected float defalutStrengthExpendInterval;

    [Header("����״̬")]
    public bool InAngryState { get; private set; }
    [Header("�˷�״̬")]
    public bool InExcitedState { get; private set; }

    [Header("������ȫ״̬")]
    public bool InSafeState { get; private set; }

    [Header("ѣ��״̬")]
    public bool InVertigoState { get; private set; }


    protected float thirstyTimer;
    #endregion

    #region �ڿ��ֶ�
    [Header("���ڿ�ֵ")]
    public float maxThristy = 100;
    [Header("��ǰ�ڿ�ֵ")]
    public float currentThristy;
    [Header("�ڿʵ�λ��Ȼ����ֵ")]
    public float thirstyExpendValue = 5;
    [Header("�ڿ���Ȼ���ļ��")]
    public float thirstyExpendInterval = 1.5f;

    public float defalutThirstyExpendInterval { get; private set; }

    protected float strengthTimer;

    #endregion

    protected bool inMyTurn;

    [Header("�������")]
    public string playerName;


    protected virtual void Start()
    {
        currentStrength = maxStrength;
        currentThristy = maxThristy;

        strengthTimer = strengthExpendInterval;
        thirstyTimer = thirstyExpendInterval;

        defalutStrengthExpendInterval = strengthExpendInterval;
        defalutThirstyExpendInterval = thirstyExpendInterval;
    }
    /// <summary>
    /// ��һ������ϻ�
    /// </summary>
    /// <param name="drink"></param>
    protected virtual void HitDrinkShop()
    {
        //Debug.Log("��һ������ϻ�");
        if (!InExcitedState)
        {
            if (!InSafeState)
                currentStrength -= HitCost;
            else
            {
                if (currentStrength - HitCost <= 20)
                    currentStrength = 20;
                else
                    currentStrength -= HitCost;
            }
        }
        else
            currentStrength += (HitCost / 2);
        //���Ż�����Ч
    }
    public virtual void DrinkIt(IDrink targetDrink)
    {
        //Debug.Log("��Һȵ�����");
        drink = targetDrink;
        drink.DrinkMe(this);
    }

    /// <summary>
    /// ������Ȼ�ָ�
    /// </summary>
    protected virtual void StrengthExpendUpdate()
    {
        if (inMyTurn)
            return;

        if (currentStrength >= 0)
        {
            if (strengthTimer >= 0)
                strengthTimer -= Time.deltaTime;
            else
            {
                currentStrength += strengthExpendValue;
                strengthTimer = strengthExpendInterval;
            }
        }
    }

    /// <summary>
    /// �ڿ�ֵ��Ȼ����
    /// </summary>
    protected virtual void ThirstExpendUpdate()
    {
        if (currentThristy >= 0)
        {
            if (thirstyTimer >= 0)
                thirstyTimer -= Time.deltaTime;
            else
            {
                if (currentThristy - thirstyExpendValue >= 0)
                    currentThristy -= thirstyExpendValue;
                else
                    currentThristy = 0;

                thirstyTimer = thirstyExpendInterval;
            }
        }

    }

    /// <summary>
    /// �����ŭ״̬
    /// </summary>
    /// <param name="angry"></param>
    public void TurnAngryState(bool angry)
    {
        if (angry)
        {
            MusicManager.Instance.PlaySound("��ɫ������ƴ���");
            InAngryState = true;
        }
        else
            InAngryState = false;
    }

    /// <summary>
    /// ���뿺��״̬
    /// </summary>
    /// <param name="excited"></param>
    public void TurnExcitedState(bool excited)
    {
        if (excited)
            InExcitedState = true;
        else
            InExcitedState = false;
    }

    /// <summary>
    /// ������ˮ��C���ж��Ƿ�ָ���20
    /// </summary>
    public void TurnSafeState(bool safe, float limitStrength)
    {
        if (currentStrength <= limitStrength)
            currentStrength = limitStrength;

        if (safe)
            InSafeState = true;
        else
            InSafeState = false;
    }

    /// <summary>
    ///����ѣ��״̬
    /// </summary>
    public void TurnVertigoState(bool vertigo)
    {
        if (vertigo)
        {
            MusicManager.Instance.PlaySound("��ɫ������ƴ���");
            GetComponent<SpriteRenderer>().color = Color.yellow;
            Transform child = transform.Find("dizzy");
            if (child != null)
            {
                child.gameObject.SetActive(true);
            }

            InVertigoState = true;
        }
        else
        {
            Transform child = transform.Find("dizzy");
            if (child != null)
            {
                child.gameObject.SetActive(false);
            }
            GetComponent<SpriteRenderer>().color = Color.white;
            InVertigoState = false;
        }
    }

}
