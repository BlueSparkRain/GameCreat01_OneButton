using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoSingleton<GameLogic>
{
    [Header("���˻غ�ʱ��")]
    public float RandDuration = 6;
    float playerTurnTimer;

    [Header("�������")]
    public int playerNum = 2;

    [Header("��ǰ�غ�������")]
    public int currentPlayerIndex = 0;
    [Header("���ֻغ��������")]
    public string playerName = "A";

    public bool gameOver = true;

    [Header("�غϵ���ʱText")]
    public TMP_Text randTimerText;
    [Header("��һغ�Text")]
    public TMP_Text randPlayerText;

    [Header("��ѧ���")]
    public Transform techPanle;

    int winnerIndex;
    string winnerName;

    private void Start()
    {
        StartCoroutine(TechPanelOpen());
    }

    /// <summary>
    /// winΪ0��ʾ�����ʧ�ܣ�1��ʾ����һ�ʤ
    /// </summary>
    /// <param name="win"></param>
    /// <param name="winner"></param>
    public void GetWinner(int win, Player winner)
    {
        //����ʤ�����漰����
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
            winnerName = "���һ";
        }
        else 
        {
            winnerName = "��Ҷ�"; 
        }
        Debug.Log(winnerName+"��ʤ��");
        //ʤ������Ͷ���
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
    /// ������Ϸ
    /// </summary>
    void GameBegin()
    {
        gameOver = false;
        playerTurnTimer = RandDuration;
        //������Ϸ��ʼ��Ч

        Debug.Log("��Ϸ��ʼ");
        randPlayerText.text = playerName;
    }

    private void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {//������Ϸ
                gameOver = false;
                StartCoroutine(TechPanelClose());
                EventCenter.Instance.EventTrigger(E_EventType.E_GameBegin);
            }
        }
       else
        {
            //��Ұ��¿ո��,����HitDrinkMachine�¼�
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
    /// ��һغ��л�
    /// </summary>
    void ChangeTurn()
    {
        //�л��غϵ���
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
    /// ��Ϸ����
    /// </summary>
    void GameOver(Player winner)
    {
        //�����������


    }
}
