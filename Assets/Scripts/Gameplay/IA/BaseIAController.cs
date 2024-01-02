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

    private void FixedUpdate()
    {
        if (!canMove)
            return;

        Vector3 dir = new Vector3(0, Mathf.Clamp(currentBall.transform.position.y - transform.position.y, -speed * Time.deltaTime, speed * Time.deltaTime));

        plataformRig.MovePosition(transform.position + dir);
    }

    private void OnSpawnBall(BaseBall ball)
    {
        currentBall = ball;
    }
}
