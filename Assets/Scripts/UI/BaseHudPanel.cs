using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BaseHudPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text fightTurnText;
    [SerializeField] private Button fleeBtn;
    [SerializeField] private Button skipBtn;

    public void SetTurnText(int idx)
    {
        fightTurnText.SetText($"{idx + 1} / 6");
    }

    public void InitSkipButtons(UnityAction flee, UnityAction skip)
    {
        fleeBtn.onClick.AddListener(flee);
        skipBtn.onClick.AddListener(skip);
    }
}
