using System.Collections;
using UnityEngine;

public class SparklingWater : Drinkable
{
    public override void DrinkMe(People people)
    {
        base.DrinkMe(people);
        Debug.Log("����ˮЧ������");
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
