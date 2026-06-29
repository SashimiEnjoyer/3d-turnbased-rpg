using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonSkill : MonoBehaviour
{
    [SerializeField] private Button skillBtn;
    [SerializeField] private Image buttonImg;
    [SerializeField] private GameObject activeIndicator;
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private TMP_Text costText;

    public void AssignButtonAction(UnityAction action)
    {
        skillBtn.onClick.AddListener(action);
    }

    public void SetActiveIndicatorState(bool state) => activeIndicator.SetActive(state);

    public void SetButtonSkill(AttackPatternConfig config)
    {
        buttonImg.sprite = config.attackImage;
        buttonText.SetText(config.name);
        costText.SetText($"Mana: {config.cost}");
    }

}
