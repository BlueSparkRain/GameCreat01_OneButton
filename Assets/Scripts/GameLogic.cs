using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoSingleton<GameLogic>
{
    [Header("个人回合时间")]
    public float RandDuration = 6;
    float playerTurnTimer;

    [Header("玩家人数")]
    public int playerNum = 2;

    [Header("当前回合玩家序号")]
    public int currentPlayerIndex = 0;
    [Header("先手回合玩家姓名")]
    public string playerName = "A";

    public bool gameOver = true;

    [Header("回合倒计时Text")]
    public TMP_Text randTimerText;
    [Header("玩家回合Text")]
    public TMP_Text randPlayerText;

    [Header("教学面板")]
    public Transform techPanle;

    int winnerIndex;
    string winnerName;

    private void Start()
    {
        StartCoroutine(TechPanelOpen());
    }

    /// <summary>
    /// win为0表示本玩家失败，1表示本玩家获胜
    /// </summary>
    /// <param name="win"></param>
    /// <param name="winner"></param>
    public void GetWinner(int win, Player winner)
    {
        //播放胜利界面及动画
        if (win == 0)
        {
            if (currentPlayerIndex == 0)
                winnerIndex = 1;
            else
                winnerIndex = 0;
        }
        else if (win == 1) 
        {
            if (currentPlayerIndex == 0)
                winnerIndex = 0;
            else
                winnerIndex = 1;
        }
        GameOver(winnerIndex);
    }

    void GameOver(int winnerIndex) 
    {
        
        if(winnerIndex==0)
        {
            winnerName = "玩家一";
        }
        else 
        {
            winnerName = "玩家二"; 
        }
        Debug.Log(winnerName+"获胜了");
        //胜利界面和动画
    }

    IEnumerator TechPanelOpen() 
    {
        yield return new WaitForSeconds(0.4f);
        yield return UITween.Instance.UIDoFade(techPanle,0,1,0.3f);
        yield return UITween.Instance.UIDoMove(techPanle, new Vector2(0, 800), Vector2.zero, 0.3f);
    }
    IEnumerator TechPanelClose() 
    {
        yield return UITween.Instance.UIDoFade(techPanle, 1, 0, 0.5f);        
        yield return UITween.Instance.UIDoMove(techPanle, Vector2.zero, new Vector2(0, 800), 0.5f);
    } 

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener(E_EventType.E_GameBegin, GameBegin);
        EventCenter.Instance.AddEventListener<Player>(E_EventType.E_GameOver, GameOver);
    }
    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener(E_EventType.E_GameBegin, GameBegin);
        EventCenter.Instance.RemoveEventListener<Player>(E_EventType.E_GameOver, GameOver);
    }

    /// <summary>
    /// 开启游戏
    /// </summary>
    void GameBegin()
    {
        gameOver = false;
        playerTurnTimer = RandDuration;
        //播放游戏开始音效

        Debug.Log("游戏开始");
        randPlayerText.text = playerName;
    }

    private void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {//开启游戏
                gameOver = false;
                StartCoroutine(TechPanelClose());
                EventCenter.Instance.EventTrigger(E_EventType.E_GameBegin);
            }
        }
       else
        {
            //玩家按下空格键,触发HitDrinkMachine事件
            if (Input.GetKeyDown(KeyCode.Space))
                EventCenter.Instance.EventTrigger(E_EventType.E_HitDrinkMachine);

            if (playerTurnTimer >= 0)
            {
                playerTurnTimer -= Time.deltaTime;
                randTimerText.text = ((int)playerTurnTimer).ToString();
            }
            else
            {
                playerTurnTimer = RandDuration;
                ChangeTurn();
            }
        }
    }

    /// <summary>
    /// 玩家回合切换
    /// </summary>
    void ChangeTurn()
    {
        //切换回合弹窗
        if (currentPlayerIndex + 1 >= playerNum)
        {
            currentPlayerIndex = 0;
            playerName = "A";
        }
        else
        {
            currentPlayerIndex++;
            playerName = "B";
        }
        randPlayerText.text = playerName;
    }

    /// <summary>
    /// 游戏结束
    /// </summary>
    void GameOver(Player winner)
    {
        //弹出结束面板


    }
}
