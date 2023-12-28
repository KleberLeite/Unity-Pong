using UnityEngine;

public class BaseBall : MonoBehaviour
{
    [Header("Ball Settings")]
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;

    [Header("Events")]
    [SerializeField] private VoidEventSO onStart;

    private Rigidbody2D ballRig;

    private Vector2 currentDir;
    private System.Random rng;
    private bool started;

    private void OnEnable()
    {
        onStart.OnEvent += OnStart;
    }

    private void OnDisable()
    {
        onStart.OnEvent -= OnStart;
    }

    private void Awake()
    {
        rng = new System.Random();
        ballRig = GetComponent<Rigidbody2D>();
    }

    virtual protected void OnStart()
    {
        currentDir = GetRandomDirection();
        started = true;
    }

    protected Vector2 GetRandomDirection()
    {
        var rngHorizontalDir = rng.Next(0, 2) == 0 ? -1 : 1;
        var rngVerticalDir = rng.Next(0, 2) == 0 ? -1 : 1;

        return new Vector2(rngHorizontalDir, rngVerticalDir);
    }

    virtual protected void Update()
    {
        if (!started)
            return;

        Vector3 incrementPos = new Vector3(
            currentDir.x * horizontalSpeed,
            currentDir.y * verticalSpeed
        ) * Time.deltaTime;
        Vector3 newPos = incrementPos + transform.position;
        ballRig.MovePosition(newPos);
    }

    virtual protected void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(GameplayConsts.WALL_TAG))
            OnCollisionWithWall();
    }

    virtual protected void OnCollisionWithWall()
    {
        currentDir.y *= -1;
    }

    virtual public void OnCollisionWithPlayer(Direction playerDirection)
    {
        float xDir = -currentDir.x;
        float yDir = GetYAxisDirWhenCollidsWithPlayer(playerDirection);

        currentDir = new Vector2(xDir, yDir);
    }

    virtual protected float GetYAxisDirWhenCollidsWithPlayer(Direction playerDirection)
        => playerDirection switch
        {
            Direction.None => currentDir.y,
            Direction.Up => 1,
            Direction.Down => -1,
            _ => throw new System.Exception() // Never
        };
}
