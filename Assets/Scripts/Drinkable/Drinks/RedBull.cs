using System.Collections;
using UnityEngine;

public class RedBull :Drinkable
{
    //��ţ���ƣ��ָ���������ֵ������ʱ���ڿڿ�ֵ�ӿ콵��
    //�ڿ�ֵ�ӿ콵�ͣ�ͨ���޸Ŀڿ���Ȼ�½����
    [Header("��ţ-��ҿڿʼ��")]
    public float changeThirstyInterval = 0.5f;

    public override void DrinkMe(People people)
    {
        base.DrinkMe(people);
        Debug.Log("��ţЧ������");
    }

    public override void MyEffect(People people)
    {
        base.MyEffect(people);
        people.thirstyExpendInterval = changeThirstyInterval;
    }

    protected override IEnumerator DrinkOver(People people)
    {
        yield return base.DrinkOver(people);
        people.thirstyExpendInterval = people.defalutThirstyExpendInterval;
        Debug.Log("��ţЧ������");
    }
}
