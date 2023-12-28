using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Direction")]
public class DirectionEventSO : ScriptableObject
{
    public UnityAction<Direction> OnEvent;

    public void Raise(Direction arg0) => OnEvent?.Invoke(arg0);
}

