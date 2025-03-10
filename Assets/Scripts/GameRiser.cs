using UnityEngine;

public class GameRiser : MonoBehaviour
{
    void Start()
    {
        UIManager.Instance.ShowPanel<MenuPanel>(null);

        MusicManager.Instance.PlayBKMusic("BK");
    }
}
