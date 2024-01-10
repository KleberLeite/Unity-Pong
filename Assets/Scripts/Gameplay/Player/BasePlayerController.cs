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
        float translation = speed * Time.deltaTime * GetScaleByDirection(currentDirection);
        float targetY = transform.position.y + translation;
        targetY = Mathf.Clamp(targetY, -positionClamp, positionClamp);

        transform.position = new Vector3(transform.position.x, targetY);
    }

    protected float GetScaleByDirection(Direction direction)
        => direction switch
        {
            Direction.None => 0,
            Direction.Up => 1,
            Direction.Down => -1,
            _ => throw new System.NotImplementedException()
        };

    private Direction GetDirection()
    {
        return (Direction)input.Current;
    }
}
