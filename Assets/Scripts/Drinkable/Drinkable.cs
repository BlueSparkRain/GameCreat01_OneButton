using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

public abstract class Drinkable : MonoBehaviour, IDrink,IHaveEffect
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

    [Header("Buff持续时间")]
    public float effectDuration = 5;


    protected virtual IEnumerator DrinkOver(People people) 
    {
        yield return new WaitForSeconds(effectDuration);
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
    public virtual void DrinkMe(People people)
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

        //触发饮料效果
        MyEffect(people);
        //开启效果结束协程
        StartCoroutine(DrinkOver(people));
    }

    /// <summary>
    /// 饮料坠落
    /// </summary>
    public void DrinkFall(int playerIndex)
    {
        //Debug.Log("饮料坠落！");
        rb.bodyType = RigidbodyType2D.Dynamic;
        if (playerIndex == 1)
        {
            transform.position = new Vector3(0f, -2.5f, -3f);
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }
            rb.velocity = new Vector2(Random.Range(0.5f, 1.0f), Random.Range(0.3f, 1.0f)).normalized * 20;
        }
            
        else if (playerIndex == 0)
        {
            transform.position = new Vector3(0f, -2.5f, -3f);
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }
            rb.velocity = new Vector2(Random.Range(-1.5f, -1.0f), Random.Range(1.0f, 1.5f)).normalized * 25;
        }

        hasFall = true;
        rb.angularVelocity = 1000;

        Destroy(transform.gameObject, 10);
        //播放坠落音效
        //饮料坠落动画
    }

    /// <summary>
    /// 饮料被玩家击打
    /// </summary>
    public void HitMe(Player player)
    {

        if (!player.InAngryState)
        {
            Debug.Log(player.playerName+"玩家未发怒");
            currentHitTime++;
        }
        else
        {
            Debug.Log(player.playerName+"玩家发怒了");
            currentHitTime += 2;

        }
        rb.velocity = new Vector2(Random.Range(-0.1f, 0.3f), Random.Range(0.2f, 0.4f)).normalized * 25;
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

    public virtual void MyEffect(People people){ }
}
