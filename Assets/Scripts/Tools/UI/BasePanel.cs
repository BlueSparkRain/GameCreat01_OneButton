using System.Collections;
using UnityEngine;
[RequireComponent(typeof(CanvasGroup))]
public abstract class BasePanel : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public float transTime = 1;

    /// <summary>
    /// �����붯�������߼�
    /// </summary>
    /// <returns></returns>
    /// <summary>
    /// ���رյ���
    /// </summary>
    public abstract IEnumerator ShowPanelTweenEffect();
    /// <summary>
    /// �����ʾ����
    /// </summary>
    public virtual void ShowPanel()
    {
        transform.SetAsLastSibling();
        Init();
    }
    /// <summary>
    /// ����˳����������߼�
    /// </summary>
    public abstract IEnumerator HidePanelTweenEffect();
    /// <summary>
    /// ����˳�����
    /// </summary>
    public virtual void HidePanel()
    {

    }

    protected virtual void Init() { }

}
