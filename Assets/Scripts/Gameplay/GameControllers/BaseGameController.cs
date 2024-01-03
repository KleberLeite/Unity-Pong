using System.Collections;
using TMPro;
using UnityEngine;

public class BaseGameController : MonoBehaviour
{
    [Header("Players Settings")]
    [SerializeField] private Transform leftPlataformHolder;
    [SerializeField] private Transform rightPlataformHolder;
    [SerializeField] private BaseIAController iaPrefab;
    [SerializeField] private BasePlayerController playerPrefab;
    [SerializeField] private InputController leftPlayerInput;
    [SerializeField] private InputController rightPlayerInput;

    [Header("Ball Settings")]
    [SerializeField] private BaseBall ballPrefab;
    [SerializeField] private Transform ballHolder;

    [Header("PlayersCount Events")]
    [SerializeField] private IntEventSO onPlayersCountChanged;

    [Header("Game Events")]
    [SerializeField] private BallEventSO onSpawnBall;
    [SerializeField] private VoidEventSO onGameStart;
    [SerializeField] private VoidEventSO onRoundEnd;
    [SerializeField] private GoalSideEventSO onGoal;

    [Header("Goals Settings")]
    [SerializeField] private TMP_Text leftGoalsText;
    [SerializeField] private TMP_Text rightGoalsText;

    private BaseBall currentBall;
    private IEnumerator countdown;

    private int goalsLeft;
    private int goalsRight;

    private enum GameState
    {
        PreparingGame,
        PreparingRound,
        Playing
    }

    private void OnEnable()
    {
        onGoal.OnEvent += OnGoal;

        onPlayersCountChanged.OnEvent += OnPlayersCountChanged;
    }

    private void OnDisable()
    {
        onGoal.OnEvent -= OnGoal;

        onPlayersCountChanged.OnEvent -= OnPlayersCountChanged;
    }

    private void ChangeState(GameState newState)
    {
        switch (newState)
        {
            case GameState.PreparingRound:
                PrepareRound();
                break;
            case GameState.Playing:
                onGameStart.Raise();
                break;
        }
    }

    private void Start()
    {
        ChangeState(GameState.PreparingGame);
        PrepareGame(0);
    }

    private void OnPlayersCountChanged(int count)
    {
        PrepareGame(count);
    }

    private void PrepareGame(int playersCount)
    {
        Debug.Log($"GameController: Preparing game with {playersCount} players");

        DestroyCurrentArena();

        if (countdown != null)
        {
            StopCoroutine(countdown);
            countdown = null;
        }

        SpawnPlayers(playersCount);

        goalsLeft = 0;
        goalsRight = 0;
        UpdateGoalsText();

        ChangeState(GameState.PreparingRound);
    }

    private void DestroyCurrentArena()
    {
        if (leftPlataformHolder.childCount != 0)
            Destroy(leftPlataformHolder.GetChild(0).gameObject);
        if (rightPlataformHolder.childCount != 0)
            Destroy(rightPlataformHolder.GetChild(0).gameObject);
        if (currentBall)
            Destroy(currentBall.gameObject);
    }

    private void SpawnPlayers(int count)
    {
        switch (count)
        {
            case 0:
                SpawnIA(leftPlataformHolder);
                SpawnIA(rightPlataformHolder);
                break;
            case 1:
                SpawnPlayer(leftPlataformHolder, leftPlayerInput);
                SpawnIA(rightPlataformHolder);
                break;
            case 2:
                SpawnPlayer(leftPlataformHolder, leftPlayerInput);
                SpawnPlayer(rightPlataformHolder, rightPlayerInput);
                break;
        }
    }

    private void SpawnPlayer(Transform holder, InputController input)
    {
        BasePlayerController player = Instantiate(playerPrefab, holder);
        player.Init(input);
    }

    private void SpawnIA(Transform holder)
    {
        Instantiate(iaPrefab, holder);
    }

    private void PrepareRound()
    {
        onRoundEnd.Raise();

        SpawnBall();

        countdown = Countdown();
        StartCoroutine(countdown);
    }

    private void SpawnBall()
    {
        currentBall = Instantiate(ballPrefab, ballHolder);
        onSpawnBall.Raise(currentBall);
    }

    private IEnumerator Countdown()
    {
        for (int i = 3; i >= 0; i--)
        {
            if (i == 0)
            {
                Debug.Log("GameController: Round started!");
                yield return new WaitForSeconds(.3f);
            }
            else
            {
                Debug.Log($"GameController: Round starting in {i}");
                yield return new WaitForSeconds(1f);
            }

        }

        ChangeState(GameState.Playing);
    }

    private void OnGoal(GoalSide side)
    {
        switch (side)
        {
            case GoalSide.Left:
                goalsLeft++;
                break;
            case GoalSide.Right:
                goalsRight++;
                break;
        }

        UpdateGoalsText();
        ChangeState(GameState.PreparingRound);
    }

    private void UpdateGoalsText()
    {
        leftGoalsText.text = goalsLeft.ToString("00");
        rightGoalsText.text = goalsRight.ToString("00");
    }
}
