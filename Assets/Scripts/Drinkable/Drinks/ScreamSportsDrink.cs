using System.Collections;
using UnityEngine;

public class ScreamSportsDrink : Drinkable
{
    //尖叫运动饮料机制：短时间内玩家每敲击一次恢复体力值

    public override void DrinkMe(People people)
    {
        base.DrinkMe(people);
        MusicManager.Instance.PlaySound("获得饮料时触发");
        Debug.Log("尖叫运动饮料效果开启");
    }

    public override void MyEffect(People people)
    {
        base.MyEffect(people);
        people.TurnExcitedState(true);
    }

    protected override IEnumerator DrinkOver(People people)
    {
        yield return base.DrinkOver(people);
        people.TurnExcitedState(false);
    }
}
