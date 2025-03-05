using System.Collections;
using UnityEngine;

public class Capsicol : Drinkable
{
    //辣椒油机制：使玩家进入红温状态
    //红温：一次敲击算两次
    public override void DrinkMe(People people)
    {
        base.DrinkMe(people);
        Debug.Log("辣椒油效果开启");
    }

    public override void MyEffect(People people)
    {
        base.MyEffect(people);
        people.TurnAngryState(true);
    }

    protected override IEnumerator DrinkOver(People people)
    {
        yield return base.DrinkOver(people);
        people.TurnAngryState(false);
        Debug.Log("辣椒油效果结束");
    }
}
