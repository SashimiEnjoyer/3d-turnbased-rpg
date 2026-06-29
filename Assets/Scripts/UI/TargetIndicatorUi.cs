using UnityEngine;

public class TargetIndicatorUi : MonoBehaviour
{
    [SerializeField] private RectTransform indicatorRect;
    private Transform target;

    public void MoveIndicatorToTarget(Transform target)
    {
        this.target = target;
    }

    private void Update()
    {
        if (target == null)
            return;

        Vector3 objectWorldPosition = target.position;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(objectWorldPosition);

        indicatorRect.position = screenPosition;

        transform.Rotate(Vector3.forward * 30 * Time.deltaTime);
    }
}
