using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/GoalSide")]
public class GoalSideEventSO : ScriptableObject
{
    public UnityAction<GoalSide> OnEvent;

    public void Raise(GoalSide arg0) => OnEvent?.Invoke(arg0);
}


