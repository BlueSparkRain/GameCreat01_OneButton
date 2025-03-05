using UnityEditor.Rendering;
using UnityEngine;

public abstract class Drinkable : MonoBehaviour, IDrink
{
    [Header("得分")]
    public int score = 100;
    [Header("至少敲击数")]
    public int minHitTime = 7;
    [Header("至多敲击数")]
    public int maxHitTime = 10;
    [Header("可恢复口渴值")]
    public float ThirstValue;
    [Header("可恢复体力值")]
    public float StrengthValue;
    [Header("真实需敲击次数")]
    public int HitTime;
    [Header("当前敲击次数")]
    public int currentHitTime = 1;

    Rigidbody2D rb;
    public bool hasFall = false;

    BoxCollider2D col;

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener(E_EventType.E_HitDrinkMachine, HitMe);
    }
    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener(E_EventType.E_HitDrinkMachine, HitMe);
    }

    public virtual void Awake()
    {
        HitTime = GetRabdomHitTime();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.GetComponent<Player>())
            EventCenter.Instance.EventTrigger(E_EventType.E_NewDrink, this);
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (!other.gameObject.GetComponent<Player>())
            EventCenter.Instance.EventTrigger(E_EventType.E_LastDrink, this);
    }

    /// <summary>
    /// 喝饮料
    /// </summary>
    /// <param name="people"></param>
    public void DrinkMe(People people)
    {
        Debug.Log("饮料被喝掉了！");
        if(people.currentStrength+StrengthValue<=100)
        people.currentStrength += StrengthValue;
        else
        people.currentStrength=100;

        if (people.currentThristy+ThirstValue<=100)
        people.currentThristy += ThirstValue;
        else
        people.currentThristy=100;
        //播放喝饮料音效
    }

    /// <summary>
    /// 饮料坠落
    /// </summary>
    public void DrinkFall(int playerIndex)
    {
        Debug.Log("饮料坠落！");
        rb.bodyType = RigidbodyType2D.Dynamic;
        if (playerIndex == 1)
            rb.velocity = new Vector2(Random.Range(0.5f, 1.0f), Random.Range(0.3f, 1.0f)).normalized * 20;
        else if (playerIndex == 0)
            rb.velocity = new Vector2(Random.Range(-1.5f, -1.0f), Random.Range(1.0f, 1.5f)).normalized * 25;

        hasFall = true;
        rb.angularVelocity = 1000;

        Destroy(transform.gameObject, 4);
        //播放坠落音效
        //饮料坠落动画
    }

    /// <summary>
    /// 饮料被击打
    /// </summary>
    public void HitMe()
    {
        Debug.Log("饮料受击！");
        currentHitTime++;
        //播放饮料晃动音效
    }

    /// <summary>
    /// 随机敲击数
    /// </summary>
    /// <returns></returns>
    protected int GetRabdomHitTime()
    {
        return Random.Range(minHitTime, maxHitTime);
    }
}
