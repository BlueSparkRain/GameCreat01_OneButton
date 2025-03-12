using System.Collections;
using UnityEngine;

public class SparklingWater : Drinkable
{
    public override void DrinkMe(People people)
    {
        base.DrinkMe(people);
        MusicManager.Instance.PlaySound("获得饮料时触发");
        Debug.Log("气泡水效果开启");
    }

    public override void MyEffect(People people)
    {
        base.MyEffect(people);
    }

    protected override IEnumerator DrinkOver(People people)
    {
        yield return base.DrinkOver(people);
    }
}
