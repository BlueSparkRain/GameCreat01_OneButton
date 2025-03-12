using System.Collections;
using TMPro;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    static private GameLogic instance;
    bool gameHasOver;
    public static GameLogic Instance
    {
        get
        {
            if (instance != null)
                return instance;
            else
                instance = new GameLogic();
            return instance;
        }
    }

    public bool gameBegin;


    private void Awake()
    {
        instance = this;
    }

    [Header("个人回合时间")]
    public float RandDuration = 6;
    float playerTurnTimer;

    [Header("玩家人数")]
    public int playerNum = 2;

    [Header("当前回合玩家序号")]
    public int currentPlayerIndex = 0;
    [Header("先手回合玩家姓名")]
    public string playerName = "A";

    bool gameOver = true;

    [Header("回合倒计时Text")]
    public TMP_Text randTimerText;
    [Header("玩家回合Text")]
    public TMP_Text randPlayerText;

    [Header("教学面板")]
    public Transform techPanle;

    [Header("玩家一")]
    public Player player1;
    [Header("玩家二")]
    public Player player2;

    [Header("当前玩家")]
    public Player currentPlayer;

    int winnerIndex;
    string winnerName;

    public Material matl;
    public Material mat2;
    public Material defaultMat;

    private void Start()
    {
        StartCoroutine(TechPanelOpen());
    }

    /// <summary>
    /// win为0表示本玩家失败，1表示本玩家获胜
    /// </summary>
    /// <param name="win"></param>
    /// <param name="player"></param>
    public void GetWinner(bool win, Player player)
    {
        if (gameHasOver)
            return;
        gameHasOver = true;
        if (win)
            GameOver(player);
        else
        {
            if (player == player1)
                GameOver(player2);
            else
                GameOver(player1);
        }
    }

    /// <summary>
    /// 开启教学面板
    /// </summary>
    /// <returns></returns>
    IEnumerator TechPanelOpen()
    {
        DialogueManager.Instance.InitIndex();
        yield return new WaitForSeconds(0.4f);
        Time.timeScale = 0;
        yield return UITween.Instance.UIDoFade(techPanle, 0, 1, 0.3f);
        yield return UITween.Instance.UIDoMove(techPanle, new Vector2(0, 800), Vector2.zero, 0.3f);

    }

    /// <summary>
    /// 关闭教学面板
    /// </summary>
    /// <returns></returns>
    public IEnumerator TechPanelClose()
    {
        yield return UITween.Instance.UIDoFade(techPanle, 1, 0, 0.5f);
        yield return UITween.Instance.UIDoMove(techPanle, Vector2.zero, new Vector2(0, 800), 0.5f);
        Time.timeScale = 1;
        gameBegin = true;
        ChangePlayerMat(matl);
        ReSetPlayerMat(player2);
    }

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener(E_EventType.E_GameBegin, GameBegin);
        //EventCenter.Instance.AddEventListener<Player>(E_EventType.E_GameOver, GameOver);
    }
    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener(E_EventType.E_GameBegin, GameBegin);
        //EventCenter.Instance.RemoveEventListener<Player>(E_EventType.E_GameOver, GameOver);
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
        //左方先手
        currentPlayer = player1;
        randPlayerText.text = currentPlayer.playerName;
    }

    private void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {//开启游戏
                gameOver = false;
                //StartCoroutine(TechPanelClose());
                EventCenter.Instance.EventTrigger(E_EventType.E_GameBegin);
            }
        }
        else
        {
            if (!gameBegin)
                return;

            //玩家按下空格键,触发HitDrinkMachine事件
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!currentPlayer.InVertigoState)
                {
                    MusicManager.Instance.PlaySound("敲击音效按空格触发");
                    EventCenter.Instance.EventTrigger(E_EventType.E_HitDrinkMachine);
                }
            }

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
            currentPlayer = player1;
            ChangePlayerMat(matl);
            ReSetPlayerMat(player2);
            player1.InMyTurn(true);
            player2.InMyTurn(false);
        }
        else
        {
            currentPlayerIndex++;
            currentPlayer = player2;
            ChangePlayerMat(mat2);
            ReSetPlayerMat(player1);
            player2.InMyTurn(true);
            player1.InMyTurn(false);
        }

        playerName = currentPlayer.playerName;
        randPlayerText.text = playerName;
    }

    SpriteRenderer GetTargetMat(Transform target)
    {
        return target?.GetComponent<SpriteRenderer>();
    }

    void ChangePlayerMat(Material mat)
    {
        Transform sp = currentPlayer.transform.GetChild(1);
        GetTargetMat(sp.GetChild(0)).material = mat;
        GetTargetMat(sp.GetChild(1)).material = mat;
        GetTargetMat(sp.GetChild(2)).material = mat;
        GetTargetMat(sp.GetChild(3)).material = mat;
        GetTargetMat(sp.GetChild(4)).material = mat;
    }

    void ReSetPlayerMat(Player player) 
    {
        Transform sp = player.transform.GetChild(1);
        GetTargetMat(sp.GetChild(0)).material = defaultMat;
        GetTargetMat(sp.GetChild(1)).material = defaultMat;
        GetTargetMat(sp.GetChild(2)).material = defaultMat;
        GetTargetMat(sp.GetChild(3)).material = defaultMat;
        GetTargetMat(sp.GetChild(4)).material = defaultMat;
    }


    /// <summary>
    /// 游戏结束
    /// </summary>
    void GameOver(Player winner)
    {
        //弹出结束面板
        UIManager.Instance.ShowPanel<GameOverPanel>(panel => { panel.GetWinner(winner); });
        Time.timeScale = 0;
    }
}
