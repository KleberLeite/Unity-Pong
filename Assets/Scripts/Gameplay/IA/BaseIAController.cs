using UnityEngine;

public class BaseIAController : BasePlataform
{
    [Header("Events")]
    [SerializeField] private BallEventSO onSpawnBall;

    private BaseBall currentBall;

    protected override void OnEnable()
    {
        base.OnEnable();

        onSpawnBall.OnEvent += OnSpawnBall;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        onSpawnBall.OnEvent -= OnSpawnBall;
    }

    private void OnSpawnBall(BaseBall ball)
    {
        currentBall = ball;
    }

    protected override Direction GetDirection()
    {
        if (currentBall.transform.position.y >= transform.position.y)
            return Direction.Up;

        return Direction.Down;
    }
}
