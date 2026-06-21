using UnityEngine;

public class TargetIndicatorUi : MonoBehaviour
{
    [SerializeField] private RectTransform indicatorRect;

    public void MoveIndicatorToTarget(Transform target)
    {
        Vector3 objectWorldPosition = target.position;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(objectWorldPosition);


        indicatorRect.position = screenPosition;
    }
}
