using UnityEngine;

public class BasePlataform : MonoBehaviour
{
    [Header("----- BasePlayer -----")]
    [Header("Movement Settings")]
    [SerializeField] protected float speed;
    [SerializeField] private float maxDisplacementInYAxis;
    [SerializeField] private GameObject plataformGO;

    [Header("Events")]
    [SerializeField] private VoidEventSO onGameStart;
    [SerializeField] protected VoidEventSO onReset;

    protected Rigidbody2D plataformRig;

    protected Direction currentDirection;
    protected bool canMove;

    protected Vector2[] directions = new Vector2[]
    {
        Vector2.zero,
        Vector2.up,
        Vector2.down
    };

    virtual protected void OnEnable()
    {
        onGameStart.OnEvent += OnGameStart;
    }

    virtual protected void OnDisable()
    {
        onGameStart.OnEvent -= OnGameStart;
    }

    private void Awake()
    {
        plataformRig = plataformGO.GetComponent<Rigidbody2D>();
    }

    private void OnGameStart()
    {
        canMove = true;
    }

    virtual protected void OnReset()
    {
        canMove = false;
        plataformGO.transform.localPosition = Vector3.zero;
    }

    private void Update()
    {
        currentDirection = GetDirection();

        if (canMove)
            Move();
    }

    virtual protected Direction GetDirection()
    {
        return Direction.None;
    }

    private void Move()
    {
        Vector3 incrementPos = speed * Time.deltaTime * GetVectorOfDirection(currentDirection);
        Vector3 newPos = incrementPos + plataformGO.transform.position;

        plataformRig.MovePosition(newPos);
    }

    protected Vector2 GetVectorOfDirection(Direction direction)
    {
        return directions[(int)direction];
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(GameplayConsts.BALL_TAG))
        {
            // to do: passar a referência da bola para o método
            OnCollisionWithBall();
        }
    }

    virtual protected void OnCollisionWithBall() { }
}
