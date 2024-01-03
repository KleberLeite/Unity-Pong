using TMPro;
using UnityEngine;

public class PlayersController : MonoBehaviour
{
    private const int MAX_PLAYERS = 2;
    private const string JOIN_TEXT = "Press SPACE to enter";
    private const string PLAYER_ONE_TEXT = "Player One";
    private const string PLAYER_TWO_TEXT = "Player Two";

    [Header("Settings")]
    [SerializeField] private TMP_Text leftPlayerText;
    [SerializeField] private TMP_Text rightPlayerText;

    [Header("PlayersCount Events")]
    [SerializeField] private IntEventSO onCountChange;

    private int playersCount;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            TryAddNewPlayer();

        if (Input.GetKeyDown(KeyCode.Escape))
            TryRemovePlayer();
    }

    private void TryAddNewPlayer()
    {
        if (playersCount == MAX_PLAYERS)
            return;

        playersCount++;
        UpdatePlayersTexts();

        onCountChange.Raise(playersCount);
    }

    private void TryRemovePlayer()
    {
        if (playersCount == 0)
            return;

        playersCount--;
        UpdatePlayersTexts();

        onCountChange.Raise(playersCount);
    }

    private void UpdatePlayersTexts()
    {
        switch (playersCount)
        {
            case 0:
                leftPlayerText.text = JOIN_TEXT;
                rightPlayerText.text = JOIN_TEXT;
                break;
            case 1:
                leftPlayerText.text = PLAYER_ONE_TEXT;
                rightPlayerText.text = JOIN_TEXT;
                break;
            case 2:
                leftPlayerText.text = PLAYER_ONE_TEXT;
                rightPlayerText.text = PLAYER_TWO_TEXT;
                break;
        }
    }
}
