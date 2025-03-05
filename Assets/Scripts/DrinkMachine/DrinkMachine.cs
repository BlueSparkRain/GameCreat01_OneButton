using System.Collections.Generic;
using UnityEngine;

//饮料机
public class DrinkMachine : MonoBehaviour
{
    [Header("饮料出生点")]
    public Transform drinkBornPos;
    [Header("饮料移动终点")]
    public Transform drinkEndPos;
    [Header("饮料移动时间")]
    public float drinkMoveDuration = 0.5f;

    [Header("货架上所有饮料")]
    public List<Drinkable> drinks = new List<Drinkable>();
    [Header("下一瓶饮料")]
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
    /// 补充新饮料
    /// </summary>
    public void AddNewDrink()
    {
        Debug.Log("新增一瓶饮料");
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
        Debug.Log("饮料机开始工作");
        AddNewDrink();
    }

    /// <summary>
    /// 每次敲击饮料机，饮料产生横向位移
    /// </summary>
    void HitDrinkMove()
    {
        Debug.Log("饮料机被敲击了");

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
    /// 随机产生一种新饮料
    /// </summary>
    /// <returns></returns>
    GameObject RandomDrink()
    {
        int index = Random.Range(0, 6);
        switch (index)
        {
            case 0:
                return Resources.Load<GameObject>("Prefab/Drink/可乐");
            case 1:
                return Resources.Load<GameObject>("Prefab/Drink/气泡水");
            case 2:
                return Resources.Load<GameObject>("Prefab/Drink/苏打水");
            case 3:
                return Resources.Load<GameObject>("Prefab/Drink/红牛");
            case 4:
                return Resources.Load<GameObject>("Prefab/Drink/桃汁");
            case 5:
                return Resources.Load<GameObject>("Prefab/Drink/运动饮料");
            default:
                return Resources.Load<GameObject>("Prefab/Drink/苏打水");
        }
    }
}
