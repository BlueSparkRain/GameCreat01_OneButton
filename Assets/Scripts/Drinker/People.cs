using UnityEngine;

public class People : MonoBehaviour,ICanDrink
{
    public IDrink drink;

    #region 体力字段
    [Header("最大体力值")]
    public float maxStrength=100;
    [Header("当前体力值")]
    public float currentStrength;

    [Header("单次敲击消耗体力值")]
    public float HitCost=5;

    [Header("体力单位自然消耗值")]
    public float strengthExpendValue=1/3;
    [Header("体力自然消耗间隔")]
    public float strengthExpendInterval=1;

    protected float thirstyTimer;
    #endregion

    #region 口渴字段
    [Header("最大口渴值")]
    public float maxThristy=100;
    [Header("当前口渴值")]
    public float currentThristy;
    [Header("口渴单位自然消耗值")]
    public float thirstyExpendValue=5;
    [Header("口渴自然消耗间隔")]
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
    /// 玩家击打饮料机
    /// </summary>
    /// <param name="drink"></param>
    protected virtual void HitDrinkShop() 
    {
    
        Debug.Log("玩家击打饮料机");
        //currentStrength-=strengthExpendValue;
        currentStrength-=HitCost;
        //播放击打音效
    }
    public virtual void DrinkIt(IDrink targetDrink)
    {
        Debug.Log("玩家喝到饮料");
        drink = targetDrink;
        drink.DrinkMe(this);
    }

    /// <summary>
    /// 体力自然消耗
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
    /// 口渴值自然消耗
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
