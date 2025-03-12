using System.Collections;
using UnityEngine;

public class ScreamSportsDrink : Drinkable
{
    //����˶����ϻ��ƣ���ʱ�������ÿ�û�һ�λָ�����ֵ

    public override void DrinkMe(People people)
    {
        base.DrinkMe(people);
        MusicManager.Instance.PlaySound("�������ʱ����");
        Debug.Log("����˶�����Ч������");
    }

    public override void MyEffect(People people)
    {
        base.MyEffect(people);
        people.TurnExcitedState(true);
    }

    protected override IEnumerator DrinkOver(People people)
    {
        yield return base.DrinkOver(people);
        people.TurnExcitedState(false);
    }
}
