using UnityEngine;

public class People : MonoBehaviour,ICanDrink
{
    public IDrink drink;

    #region �����ֶ�
    [Header("�������ֵ")]
    public float maxStrength=100;
    [Header("��ǰ����ֵ")]
    public float currentStrength;

    [Header("�����û���������ֵ")]
    public float HitCost=5;

    [Header("������λ��Ȼ����ֵ")]
    public float strengthExpendValue=1/3;
    [Header("������Ȼ���ļ��")]
    public float strengthExpendInterval=1;

    protected float thirstyTimer;
    #endregion

    #region �ڿ��ֶ�
    [Header("���ڿ�ֵ")]
    public float maxThristy=100;
    [Header("��ǰ�ڿ�ֵ")]
    public float currentThristy;
    [Header("�ڿʵ�λ��Ȼ����ֵ")]
    public float thirstyExpendValue=5;
    [Header("�ڿ���Ȼ���ļ��")]
    public float thirstyExpendInterval=1.5f;

    protected float strengthTimer;

    #endregion

    //private void OnEnable()
    //{
    //   EventCenter.Instance.AddEventListener(E_EventType.E_HitDrinkMachine,HitDrinkShop);
    //}

    //private void OnDisable()
    //{
    //   EventCenter.Instance.AddEventListener(E_EventType.E_HitDrinkMachine,HitDrinkShop);  
    //}

    protected virtual void Start() 
    {
       currentStrength=maxStrength;
       currentThristy=maxThristy;

       strengthTimer=strengthExpendInterval;
       thirstyTimer=thirstyExpendInterval;
    }

    /// <summary>
    /// ��һ������ϻ�
    /// </summary>
    /// <param name="drink"></param>
    protected virtual void HitDrinkShop() 
    {
    
        Debug.Log("��һ������ϻ�");
        //currentStrength-=strengthExpendValue;
        currentStrength-=HitCost;
        //���Ż�����Ч
    }
    public virtual void DrinkIt(IDrink targetDrink)
    {
        Debug.Log("��Һȵ�����");
        drink = targetDrink;
        drink.DrinkMe(this);
    }

    /// <summary>
    /// ������Ȼ����
    /// </summary>
    protected virtual  void StrengthExpendUpdate() 
    {
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
            if (thirstyTimer>= 0)
                thirstyTimer -= Time.deltaTime;
            else
            {
                currentThristy -= thirstyExpendValue;
                thirstyTimer = thirstyExpendInterval;
            }
        }

    }
}
