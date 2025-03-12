using DialogueDemo;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public enum E_DisplayType
{
    Defalut, Fading, Typing
}
public class DialogueManager :MonoBehaviour
{

    static private DialogueManager instance;
    bool gameHasOver;
    public static DialogueManager Instance
    {
        get
        {
            if (instance != null)
                return instance;
            else
                instance = new DialogueManager();
            return instance;
        }
    }
    public List<AdvancedText> lists=new List<AdvancedText>();
    public E_DisplayType showType=E_DisplayType.Typing;

    public List<string>stringList = new List<string>();
    int index;
    bool done;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (done)
                return;
            
            if (index + 1 <= lists.Count)
            {
                StartCoroutine(lists[index].ShowText(stringList[index], showType, 1));
                index++;
            }
            else
            {
                StartCoroutine(GameLogic.Instance.TechPanelClose());
                done = true;
            }
        }

    }

    public void InitIndex()
    {
        index = 0;
    }


}
