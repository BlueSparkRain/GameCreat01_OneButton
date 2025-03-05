using System.Collections.Generic;
using UnityEngine;

//���ϻ�
public class DrinkMachine : MonoBehaviour
{
    static readonly string PathDir = "Prefab/Drink/";
    static readonly string mineralWater = "ũ��ɽȪ";
    static readonly string soadWater = "�մ�ˮ";
    static readonly string sparklingWater = "����ˮ";
    static readonly string redBull = "��ţ";
    static readonly string WaterBathC = "ˮ��C";
    static readonly string screamSportsDrink = "����˶�����";
    static readonly string capsicol = "������";
    static readonly string windOil = "���;�";


    List<string> drinksPath = new List<string>() {mineralWater,soadWater,sparklingWater,redBull,WaterBathC,screamSportsDrink,capsicol,windOil };

    [Header("���ϳ�����")]
    public Transform drinkBornPos;
    [Header("�����ƶ��յ�")]
    public Transform drinkEndPos;
    [Header("�����ƶ�ʱ��")]
    public float drinkMoveDuration = 0.5f;

    [Header("��������������")]
    public List<Drinkable> drinks = new List<Drinkable>();
    [Header("��һƿ����")]
    public Drinkable nextDrink;

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener(E_EventType.E_GameBegin, GameBegin);
        EventCenter.Instance.AddEventListener<Drinkable>(E_EventType.E_NewDrink, GetNextDrink);
        EventCenter.Instance.AddEventListener<Drinkable>(E_EventType.E_LastDrink, RemoveLastDrink);
        EventCenter.Instance.AddEventListener(E_EventType.E_HitDrinkMachine, HitDrinkMove);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener(E_EventType.E_GameBegin, GameBegin);
        EventCenter.Instance.RemoveEventListener(E_EventType.E_HitDrinkMachine, HitDrinkMove);
        EventCenter.Instance.RemoveEventListener<Drinkable>(E_EventType.E_NewDrink, GetNextDrink);
        EventCenter.Instance.RemoveEventListener<Drinkable>(E_EventType.E_LastDrink, RemoveLastDrink);
    }

    /// <summary>
    /// ����������
    /// </summary>
    public void AddNewDrink()
    {
        //Debug.Log("����һƿ����");
        GameObject newDrinkObj = Instantiate(RandomDrink());
        drinks.Add(newDrinkObj.GetComponent<Drinkable>());
        newDrinkObj.transform.position = drinkBornPos.position + Vector3.up * 2;
    }

    void GetNextDrink(Drinkable drink)
    {
        nextDrink = drink;
    }

    void RemoveLastDrink(Drinkable drink)
    {
        drinks.Remove(drink);
    }

    void GameBegin()
    {
        Debug.Log("���ϻ���ʼ����");
        AddNewDrink();
    }

    /// <summary>
    /// ÿ���û����ϻ������ϲ�������λ��
    /// </summary>
    void HitDrinkMove()
    {
        //Debug.Log("���ϻ����û���");

        if (nextDrink && nextDrink.currentHitTime >= nextDrink.HitTime)
        {
            if (!nextDrink.hasFall)
                nextDrink.DrinkFall(GameLogic.Instance.currentPlayerIndex);
            if (drinks.Count < 1)
                AddNewDrink();
            return;
        }
        if (nextDrink &&!nextDrink.hasFall)
        {
            StartCoroutine(LerpHelper.MakeLerp(nextDrink.transform.position,
                Vector3.Lerp(drinkBornPos.position, drinkEndPos.position,
               (float)(nextDrink.currentHitTime / (float)nextDrink.minHitTime)), drinkMoveDuration,
               (val) => nextDrink.transform.position = val));
        }
        if(nextDrink&& GameLogic.Instance.currentPlayer)
        nextDrink.HitMe(GameLogic.Instance.currentPlayer);
    }


    /// <summary>
    /// �������һ��������
    /// </summary>
    /// <returns></returns>
    GameObject RandomDrink()
    {
        int index = Random.Range(0, drinksPath.Count);
        string path = PathDir + drinksPath[index];
        return Resources.Load<GameObject>(path);
    }
}
