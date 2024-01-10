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
        if (!canMove || !currentBall)
            return;

        float translation = currentBall.transform.position.y - transform.position.y;
        // use this to stop the AI ​​from moving faster than configured
        translation = Mathf.Clamp(translation, -speed * Time.deltaTime, speed * Time.deltaTime);
        // use this so the AI ​​doesn't leave the arena
        float targetY = transform.position.y + translation;
        targetY = Mathf.Clamp(targetY, -positionClamp, positionClamp);

        transform.position = new Vector3(transform.position.x, targetY);
    }

    private void OnSpawnBall(BaseBall ball)
    {
        currentBall = ball;
    }
}
