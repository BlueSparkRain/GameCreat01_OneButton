using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindOil : Drinkable
{
    //风油精特性：喝到眩晕10s
    public override void DrinkMe(People people)
    {
        base.DrinkMe(people);
        Debug.Log("风油精效果开启");
    }

    public override void MyEffect(People people)
    {
        base.MyEffect(people);
        people.TurnVertigoState(true);
    }

    protected override IEnumerator DrinkOver(People people)
    {
       yield return base.DrinkOver(people);
       people.TurnVertigoState(false);
    }
}
