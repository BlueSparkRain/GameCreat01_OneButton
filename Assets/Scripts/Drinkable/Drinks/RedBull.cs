using System.Collections;
using UnityEngine;

public class RedBull :Drinkable
{
    //红牛机制：恢复大量体力值，但短时间内口渴值加快降低
    //口渴值加快降低：通过修改口渴自然下降间隔
    [Header("红牛-玩家口渴间隔")]
    public float changeThirstyInterval = 0.5f;

    public override void DrinkMe(People people)
    {
        base.DrinkMe(people);
        Debug.Log("红牛效果开启");
    }

    public override void MyEffect(People people)
    {
        base.MyEffect(people);
        people.thirstyExpendInterval = changeThirstyInterval;
    }

    protected override IEnumerator DrinkOver(People people)
    {
        yield return base.DrinkOver(people);
        people.thirstyExpendInterval = people.defalutThirstyExpendInterval;
        Debug.Log("红牛效果结束");
    }
}
