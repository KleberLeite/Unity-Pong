using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Void")]
public class VoidEventSO : ScriptableObject
{
    public UnityAction OnEvent;

    public void Raise() => OnEvent?.Invoke();
}
