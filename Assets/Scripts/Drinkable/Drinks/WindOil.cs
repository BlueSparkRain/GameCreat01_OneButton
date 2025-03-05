using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindOil : Drinkable
{
    //���;����ԣ��ȵ�ѣ��10s
    public override void DrinkMe(People people)
    {
        base.DrinkMe(people);
        Debug.Log("���;�Ч������");
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
