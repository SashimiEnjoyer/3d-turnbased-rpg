using DG.Tweening;
using UnityEngine;

public class WarningPanel : MonoBehaviour
{
    private RectTransform rect;
    Tween tw;

    private void OnEnable()
    {
        if(rect == null)
            rect = GetComponent<RectTransform>();

        rect.anchoredPosition = Vector2.up * -100f;
        tw = rect.DOMoveY(25f, 1).OnComplete(() => gameObject.SetActive(false));
    }

    private void OnDisable()
    {
        tw?.Kill();
    }
}
