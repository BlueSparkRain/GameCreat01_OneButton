using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

public abstract class Drinkable : MonoBehaviour, IDrink,IHaveEffect
{
    [Header("�÷�")]
    public int score = 100;
    [Header("�����û���")]
    public int minHitTime = 7;
    [Header("�����û���")]
    public int maxHitTime = 10;
    [Header("�ɻָ��ڿ�ֵ")]
    public float ThirstValue;
    [Header("�ɻָ�����ֵ")]
    public float StrengthValue;
    [Header("��ʵ���û�����")]
    public int HitTime;
    [Header("��ǰ�û�����")]
    public int currentHitTime = 1;

    Rigidbody2D rb;
    public bool hasFall = false;

    BoxCollider2D col;

    [Header("Buff����ʱ��")]
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
    /// ������
    /// </summary>
    /// <param name="people"></param>
    public virtual void DrinkMe(People people)
    {
        Debug.Log("���ϱ��ȵ��ˣ�");
        if(people.currentStrength+StrengthValue<=100)
        people.currentStrength += StrengthValue;
        else
        people.currentStrength=100;

        if (people.currentThristy+ThirstValue<=100)
        people.currentThristy += ThirstValue;
        else
        people.currentThristy=100;
        //���ź�������Ч

        //��������Ч��
        MyEffect(people);
        //����Ч������Э��
        StartCoroutine(DrinkOver(people));
    }

    /// <summary>
    /// ����׹��
    /// </summary>
    public void DrinkFall(int playerIndex)
    {
        //Debug.Log("����׹�䣡");
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
        //����׹����Ч
        //����׹�䶯��
    }

    /// <summary>
    /// ���ϱ���һ���
    /// </summary>
    public void HitMe(Player player)
    {

        if (!player.InAngryState)
        {
            Debug.Log(player.playerName+"���δ��ŭ");
            currentHitTime++;
        }
        else
        {
            Debug.Log(player.playerName+"��ҷ�ŭ��");
            currentHitTime += 2;

        }
        rb.velocity = new Vector2(Random.Range(-0.1f, 0.3f), Random.Range(0.2f, 0.4f)).normalized * 25;
        //�������ϻζ���Ч
    }

    /// <summary>
    /// ����û���
    /// </summary>
    /// <returns></returns>
    protected int GetRabdomHitTime()
    {
        return Random.Range(minHitTime, maxHitTime);
    }

    public virtual void MyEffect(People people){ }
}
