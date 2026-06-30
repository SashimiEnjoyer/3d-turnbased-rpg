using TMPro;
using UnityEngine;
using DG.Tweening;


public class NewTurnPanel : MonoBehaviour
{
    [SerializeField] private Transform indicatorRect;
    [SerializeField] private TMP_Text turnText;

    private void OnEnable()
    {
        indicatorRect.DOPunchScale(Vector3.one * 0.1f, 0.4f, 1, 1);
    }

    public void SetCurrentTurn(int idx, int maxTurn) => turnText.SetText($"Turn {idx}/{maxTurn}");
}
