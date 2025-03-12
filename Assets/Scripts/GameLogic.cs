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

    [Header("���˻غ�ʱ��")]
    public float RandDuration = 6;
    float playerTurnTimer;

    [Header("�������")]
    public int playerNum = 2;

    [Header("��ǰ�غ�������")]
    public int currentPlayerIndex = 0;
    [Header("���ֻغ��������")]
    public string playerName = "A";

    bool gameOver = true;

    [Header("�غϵ���ʱText")]
    public TMP_Text randTimerText;
    [Header("��һغ�Text")]
    public TMP_Text randPlayerText;

    [Header("��ѧ���")]
    public Transform techPanle;

    [Header("���һ")]
    public Player player1;
    [Header("��Ҷ�")]
    public Player player2;

    [Header("��ǰ���")]
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
    /// winΪ0��ʾ�����ʧ�ܣ�1��ʾ����һ�ʤ
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
    /// ������ѧ���
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
    /// �رս�ѧ���
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
    /// ������Ϸ
    /// </summary>
    void GameBegin()
    {
        gameOver = false;
        playerTurnTimer = RandDuration;
        //������Ϸ��ʼ��Ч

        Debug.Log("��Ϸ��ʼ");
        //������
        currentPlayer = player1;
        randPlayerText.text = currentPlayer.playerName;
    }

    private void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {//������Ϸ
                gameOver = false;
                //StartCoroutine(TechPanelClose());
                EventCenter.Instance.EventTrigger(E_EventType.E_GameBegin);
            }
        }
        else
        {
            if (!gameBegin)
                return;

            //��Ұ��¿ո��,����HitDrinkMachine�¼�
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!currentPlayer.InVertigoState)
                {
                    MusicManager.Instance.PlaySound("�û���Ч���ո񴥷�");
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
    /// ��һغ��л�
    /// </summary>
    void ChangeTurn()
    {
        //�л��غϵ���
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
    /// ��Ϸ����
    /// </summary>
    void GameOver(Player winner)
    {
        //�����������
        UIManager.Instance.ShowPanel<GameOverPanel>(panel => { panel.GetWinner(winner); });
        Time.timeScale = 0;
    }
}
