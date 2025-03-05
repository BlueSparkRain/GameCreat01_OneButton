using System.Collections;
using UnityEngine;

public class WaterBathC : Drinkable
{
    //水溶C机制：短时间内玩家的体力值不会低于20
    [Header("水溶C-最低体力")]
    public float limitMinStrength=20;
    public override void DrinkMe(People people)
    {
        base.DrinkMe(people);
        Debug.Log("水溶C效果开启");
    }

    public override void MyEffect(People people)
    {
        base.MyEffect(people);
        people.TurnSafeState(true,limitMinStrength);
    }

    protected override IEnumerator DrinkOver(People people)
    {
      yield  return base.DrinkOver(people);
      people.TurnSafeState(false,limitMinStrength);
    }
}
