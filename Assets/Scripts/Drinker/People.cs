using UnityEngine;

public class People : MonoBehaviour, ICanDrink
{
    public IDrink drink;

    #region 体力字段
    [Header("最大体力值")]
    public float maxStrength = 100;
    [Header("当前体力值")]
    public float currentStrength;

    [Header("单次敲击消耗体力值")]
    public float HitCost = 5;

    [Header("体力单位自然消耗值")]
    public float strengthExpendValue = 1 / 3;
    [Header("体力自然消耗间隔")]
    public float strengthExpendInterval = 1;

    protected float defalutStrengthExpendInterval;

    [Header("红温状态")]
    public bool InAngryState { get; private set; }
    [Header("兴奋状态")]
    public bool InExcitedState { get; private set; }

    [Header("体征安全状态")]
    public bool InSafeState { get; private set; }

    [Header("眩晕状态")]
    public bool InVertigoState { get; private set; }


    protected float thirstyTimer;
    #endregion

    #region 口渴字段
    [Header("最大口渴值")]
    public float maxThristy = 100;
    [Header("当前口渴值")]
    public float currentThristy;
    [Header("口渴单位自然消耗值")]
    public float thirstyExpendValue = 5;
    [Header("口渴自然消耗间隔")]
    public float thirstyExpendInterval = 1.5f;

    public float defalutThirstyExpendInterval { get; private set; }

    protected float strengthTimer;

    #endregion

    protected bool inMyTurn;

    [Header("玩家名称")]
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
    /// 玩家击打饮料机
    /// </summary>
    /// <param name="drink"></param>
    protected virtual void HitDrinkShop()
    {
        //Debug.Log("玩家击打饮料机");
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
        //播放击打音效
    }
    public virtual void DrinkIt(IDrink targetDrink)
    {
        //Debug.Log("玩家喝到饮料");
        drink = targetDrink;
        drink.DrinkMe(this);
    }

    /// <summary>
    /// 体力自然恢复
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
    /// 口渴值自然消耗
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
    /// 进入愤怒状态
    /// </summary>
    /// <param name="angry"></param>
    public void TurnAngryState(bool angry)
    {
        if (angry)
        {
            MusicManager.Instance.PlaySound("角色脸变红或黄触发");
            InAngryState = true;
        }
        else
            InAngryState = false;
    }

    /// <summary>
    /// 进入亢奋状态
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
    /// 当喝下水溶C，判断是否恢复到20
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
    ///进入眩晕状态
    /// </summary>
    public void TurnVertigoState(bool vertigo)
    {
        if (vertigo)
        {
            MusicManager.Instance.PlaySound("角色脸变红或黄触发");
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
