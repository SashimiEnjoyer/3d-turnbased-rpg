using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ArenaHeroTurnPanel : MonoBehaviour
{
    [SerializeField] private ButtonSkill atk1Btn;
    [SerializeField] private ButtonSkill atk2Btn;
    [SerializeField] private ButtonSkill atk3Btn;
    [SerializeField] private ButtonSkill ultBtn;


    public void InitActionButtons(UnityAction atk1, UnityAction atk2, UnityAction atk3, UnityAction ult)
    {
        atk1Btn.AssignButtonAction(atk1);
        atk2Btn.AssignButtonAction(atk2);
        atk3Btn.AssignButtonAction(atk3);
        ultBtn.AssignButtonAction(ult);
    }

    public void SetButtonSkilLActiveState(int idx)
    {
        atk1Btn.SetActiveIndicatorState(idx == 0);
        atk2Btn.SetActiveIndicatorState(idx == 1);
        atk3Btn.SetActiveIndicatorState(idx == 2);
        ultBtn.SetActiveIndicatorState(idx == 3);
    }

    public void SetCurrentSkillButtons(AttackPatternConfig[] configs)
    {
        atk1Btn.SetButtonSkill(configs[0]);
        atk2Btn.SetButtonSkill(configs[1]);
        atk3Btn.SetButtonSkill(configs[2]);
        ultBtn.SetButtonSkill(configs[3]);
    }

}
