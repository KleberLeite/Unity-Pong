using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Ball")]
public class BallEventSO : ScriptableObject
{
    public UnityAction<BaseBall> OnEvent;

    public void Raise(BaseBall arg0) => OnEvent?.Invoke(arg0);
}
