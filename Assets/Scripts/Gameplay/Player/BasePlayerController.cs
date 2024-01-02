using UnityEngine;

public class BasePlayerController : BasePlataform
{
    private InputController input;

    public void Init(InputController input)
    {
        this.input = input;
    }

    protected override Direction GetDirection()
    {
        return (Direction)input.Current;
    }
}
