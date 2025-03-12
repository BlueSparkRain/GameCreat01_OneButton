using System.Collections;
using UnityEngine;

public class SodaWater : Drinkable
{
    public override void DrinkMe(People people)
    {
        base.DrinkMe(people);
        MusicManager.Instance.PlaySound("获得饮料时触发");
        Debug.Log("苏打效果开启");
    }

    protected override IEnumerator DrinkOver(People people)
    {
       yield  return base.DrinkOver(people);
    }

    public override void MyEffect(People people)
    {
        base.MyEffect(people);
    }
}
