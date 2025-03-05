using System.Collections.Generic;
using UnityEngine;

//���ϻ�
public class DrinkMachine : MonoBehaviour
{
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
        Debug.Log("����һƿ����");
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
        Debug.Log("���ϻ����û���");

        if (nextDrink != null && nextDrink.currentHitTime >= nextDrink.HitTime)
        {
            if (!nextDrink.hasFall)
                nextDrink.DrinkFall(GameLogic.Instance.currentPlayerIndex);
            if (drinks.Count < 1)
                AddNewDrink();
            return;
        }
        if (nextDrink != null&&!nextDrink.hasFall)
        {
            StartCoroutine(LerpHelper.MakeLerp(nextDrink.transform.position,
                Vector3.Lerp(drinkBornPos.position, drinkEndPos.position,
               (float)(nextDrink.currentHitTime / (float)nextDrink.minHitTime)), drinkMoveDuration,
                (val) => nextDrink.transform.position = val));
        }
    }


    /// <summary>
    /// �������һ��������
    /// </summary>
    /// <returns></returns>
    GameObject RandomDrink()
    {
        int index = Random.Range(0, 6);
        switch (index)
        {
            case 0:
                return Resources.Load<GameObject>("Prefab/Drink/����");
            case 1:
                return Resources.Load<GameObject>("Prefab/Drink/����ˮ");
            case 2:
                return Resources.Load<GameObject>("Prefab/Drink/�մ�ˮ");
            case 3:
                return Resources.Load<GameObject>("Prefab/Drink/��ţ");
            case 4:
                return Resources.Load<GameObject>("Prefab/Drink/��֭");
            case 5:
                return Resources.Load<GameObject>("Prefab/Drink/�˶�����");
            default:
                return Resources.Load<GameObject>("Prefab/Drink/�մ�ˮ");
        }
    }
}
