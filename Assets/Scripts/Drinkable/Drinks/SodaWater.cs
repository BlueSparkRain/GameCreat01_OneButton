using System.Collections;
using UnityEngine;

public class SodaWater : Drinkable
{
    public override void DrinkMe(People people)
    {
        base.DrinkMe(people);
        Debug.Log("�մ�Ч������");
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
