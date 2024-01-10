using UnityEngine;

public class BasePlataform : MonoBehaviour
{
    [Header("----- BasePlayer -----")]
    [Header("Movement Settings")]
    [SerializeField] protected float speed;
    [SerializeField] protected float positionClamp;

    [Header("Game Events")]
    [SerializeField] private VoidEventSO onGameStart;
    [SerializeField] protected VoidEventSO onReset;

    protected Direction currentDirection;
    protected bool canMove;

    virtual protected void OnEnable()
    {
        onGameStart.OnEvent += OnGameStart;
    }

    virtual protected void OnDisable()
    {
        onGameStart.OnEvent -= OnGameStart;
    }

    public void Init(GoalSide side)
    {
        if (side == GoalSide.Right)
            transform.eulerAngles = new Vector3(0, 0, 180);
    }

    private void OnGameStart()
    {
        canMove = true;
    }

    virtual protected void OnReset()
    {
        canMove = false;
        transform.localPosition = Vector3.zero;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(GameplayConsts.BALL_TAG))
        {
            BaseBall ball = col.gameObject.GetComponent<BaseBall>();
            OnCollisionWithBall(ball);
        }
    }

    virtual protected void OnCollisionWithBall(BaseBall ball)
    {
        ball.OnCollisionWithPlayer(currentDirection);
    }
}
