using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/int")]
public class IntEventSO : ScriptableObject
{
    public UnityAction<int> OnEvent;

    public void Raise(int arg0) => OnEvent?.Invoke(arg0);
}

