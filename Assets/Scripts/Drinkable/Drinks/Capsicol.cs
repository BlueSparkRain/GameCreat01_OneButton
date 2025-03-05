using System.Collections;
using UnityEngine;

public class Capsicol : Drinkable
{
    //�����ͻ��ƣ�ʹ��ҽ������״̬
    //���£�һ���û�������
    public override void DrinkMe(People people)
    {
        base.DrinkMe(people);
        Debug.Log("������Ч������");
    }

    public override void MyEffect(People people)
    {
        base.MyEffect(people);
        people.TurnAngryState(true);
    }

    protected override IEnumerator DrinkOver(People people)
    {
        yield return base.DrinkOver(people);
        people.TurnAngryState(false);
        Debug.Log("������Ч������");
    }
}
