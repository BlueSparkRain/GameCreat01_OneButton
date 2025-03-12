using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPanel : BasePanel
{
    bool IntoGame;
    public override void HidePanel()
    {
        base.HidePanel();
        IntoGame = false;
        Animator anim = GetComponentInChildren<Animator>();
        anim.SetBool("Idle",false);
    }

    public override IEnumerator HidePanelTweenEffect()
    {
        UITween.Instance.UIDoMove(transform,Vector3.zero,new Vector3(0,1000,0),transTime/2);
        yield return UITween.Instance.UIDoFade(transform,1,0,transTime);

    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        Time.timeScale = 1.0f;
        StartCoroutine(AnimPlay());
        //StartCoroutine(AnimPlay());
    }

    public override IEnumerator ShowPanelTweenEffect()
    {
       yield return  UITween.Instance.UIDoFade(transform, 0, 1, transTime);
    }

    protected override void Init()
    {
        base.Init();
    }
    
    IEnumerator AnimPlay() 
    {
        yield return new WaitForSeconds(1);
        Animator anim=GetComponentInChildren<Animator>();
        anim.SetTrigger("Appear");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        anim.SetBool("Idle",true);
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
