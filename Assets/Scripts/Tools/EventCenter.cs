using System.Collections.Generic;
using UnityEngine.Events;

public class EventCenter : BaseSingletonManager<EventCenter>
{
    /// <summary>
    /// EventCenter��Ҫ���ã����ͳ�����϶ȣ�ʹ��ͬģ����룬����Ҫֱ�����û������˴˵ľ���ʵ��
    /// ����ԭ�����Ļ����ƣ��۲���ģʽ�������ͨ��
    /// �ؼ�������
    /// 1.�������ַ����¼�
    /// 2.��ӡ��Ƴ��¼�������
    /// 3.��������¼�������
    /// </summary>
    public EventCenter() { }
    private Dictionary<E_EventType, IEventInfo> eventDic = new Dictionary<E_EventType, IEventInfo>();
    /*�޲�****************************************************************/

    public void AddEventListener(E_EventType name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
            //��Ϊ�Ǹ��ࣨIEventInfo��װ���ࣨEventInfo������asΪ���ࣨEventInfo������ʹ�����е�actions
            (eventDic[name] as EventInfo).actions += action;
        else
            eventDic.Add(name, new EventInfo(action));
    }

    public void EventTrigger(E_EventType name)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo).actions?.Invoke();
    }

    public void RemoveEventListener(E_EventType name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo).actions -= action;
    }

    /*�в�****************************************************************/
    public void AddEventListener<T>(E_EventType name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
            //��Ϊ�Ǹ��ࣨIEventInfo��װ���ࣨEventInfo<T>������asΪ���ࣨEventInfo<T>������ʹ�����е�actions
            (eventDic[name] as EventInfo<T>).actions += action;
        else
            eventDic.Add(name, new EventInfo<T>(action));
        
    }
    public void EventTrigger<T>(E_EventType name, T info)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T>).actions?.Invoke(info);
        }
    }
    public void RemoveEventListener<T>(E_EventType name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T>).actions -= action;
        }
    }
    public void ClearAllEvents()
    {
        eventDic.Clear();
    }
}

//��Ϊ�˰�����Ӧ�۲��� ����ί�е� ��
public class EventInfo : IEventInfo
{
    public UnityAction actions;

    //�����۲��� ��Ӧ�� ������Ϣ ��¼������
    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}


public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> actions;

    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}


//�˽ӿ����������滻ԭ�� װ������� ���࣬Ŀ�����ִ�����е�
public interface IEventInfo
{}

public enum E_EventType
{
    /// <summary>
    /// ��Ϸ��ʼ
    /// </summary>
    E_GameBegin,
    
    /// <summary>
    /// �¼����û����ϻ�
    /// </summary>
    E_HitDrinkMachine,
    
    /// <summary>
    /// �¼�����Ϸ����
    /// </summary>
    E_GameOver,
    
    /// <summary>
    ///���������� 
    /// </summary>
    E_NewDrink,
    
    /// <summary>
    ///�������� 
    /// </summary>
    E_LastDrink,

    /// <summary> 
    ///��Һȵ�����
    /// </summary>
    E_PlayerGetDrink,
}


