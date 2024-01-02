using UnityEngine;

public class BasePlayerController : BasePlataform
{
    private InputController input;

    public void Init(InputController input)
    {
        this.input = input;
    }

    private void Update()
    {
        if (!canMove)
            return;

        currentDirection = GetDirection();
        Move();
    }

    private void Move()
    {
        Vector3 incrementPos = speed * Time.deltaTime * GetVectorOfDirection(currentDirection);
        Vector3 newPos = incrementPos + transform.position;

        plataformRig.MovePosition(newPos);
    }

    protected Vector2 GetVectorOfDirection(Direction direction)
    {
        return directions[(int)direction];
    }

    private Direction GetDirection()
    {
        return (Direction)input.Current;
    }
}
