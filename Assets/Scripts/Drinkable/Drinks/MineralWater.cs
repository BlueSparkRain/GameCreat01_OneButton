using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralWater : Drinkable
{
    public override void DrinkMe(People people)
    {
        base.DrinkMe(people);
    }

    protected override IEnumerator DrinkOver(People people)
    {
       yield return base.DrinkOver(people);
    }

    public override void MyEffect(People people)
    {
        base.MyEffect(people);
    }
}
