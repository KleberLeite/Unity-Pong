using UnityEngine;

public class InputController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private KeyCode upKey;
    [SerializeField] private KeyCode downKey;

    public InputDirection Current { get; private set; }

    public enum InputDirection
    {
        None,
        Positive,
        Negative
    }

    private void Update()
    {
        bool pressingUp = Input.GetKey(upKey);
        bool pressingDown = Input.GetKey(downKey);

        if (pressingUp && !pressingDown)
            Current = InputDirection.Positive;
        else if (pressingDown && !pressingUp)
            Current = InputDirection.Negative;
        else
            Current = InputDirection.None;
    }
}
