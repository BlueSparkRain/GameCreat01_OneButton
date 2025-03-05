using System.Collections;
using UnityEngine;

public class WaterBathC : Drinkable
{
    //ˮ��C���ƣ���ʱ������ҵ�����ֵ�������20
    [Header("ˮ��C-�������")]
    public float limitMinStrength=20;
    public override void DrinkMe(People people)
    {
        base.DrinkMe(people);
        Debug.Log("ˮ��CЧ������");
    }

    public override void MyEffect(People people)
    {
        base.MyEffect(people);
        people.TurnSafeState(true,limitMinStrength);
    }

    protected override IEnumerator DrinkOver(People people)
    {
      yield  return base.DrinkOver(people);
      people.TurnSafeState(false,limitMinStrength);
    }
}
