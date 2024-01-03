using UnityEngine;

public class GoalDetector : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GoalSide side;

    [Header("Events")]
    [SerializeField] private GoalSideEventSO onGoal;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(GameplayConsts.BALL_TAG))
        {
            Destroy(other.gameObject);
            onGoal.Raise(side);
        }
    }
}
