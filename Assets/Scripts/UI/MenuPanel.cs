using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPanel : BasePanel
{
    bool IntoGame;
    public override void HidePanel()
    {
        base.HidePanel();
    }

    public override IEnumerator HidePanelTweenEffect()
    {
        yield return UITween.Instance.UIDoFade(transform,1,0,transTime);
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }

    public override IEnumerator ShowPanelTweenEffect()
    {
       yield return  UITween.Instance.UIDoFade(transform, 0, 1, transTime);
    }

    protected override void Init()
    {
        base.Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&!IntoGame) 
        {
            //¿ªÆôShader½çÃæ£¡
            SceneManager.LoadScene(1);
            IntoGame = true;
            UIManager.Instance.HidePanel<MenuPanel>();
        }
    }
}
