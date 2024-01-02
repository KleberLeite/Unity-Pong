using UnityEngine;

public class BasePlayerController : BasePlataform
{
    [Header("Input Settings")]
    [SerializeField] private InputController input;

    protected override Direction GetDirection()
    {
        return (Direction)input.Current;
    }
}
