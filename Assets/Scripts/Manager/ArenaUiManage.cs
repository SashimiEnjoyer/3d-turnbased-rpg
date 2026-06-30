
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArenaUiManage : MonoBehaviour
{
    [SerializeField] private GameObject charaSeqUiPrefab;
    [SerializeField] private Transform seqUiParent;
    [SerializeField] private ArenaHeroTurnPanel arenaHeroTurnPanel;
    [SerializeField] private WarningPanel warningPanel;
    [SerializeField] private BaseHudPanel hudPanel;
    [SerializeField] private NewTurnPanel newTurnPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private CharacterDetailPanel[] heroDetailPanels;
    [SerializeField] private CharacterDetailPanel[] enemyDetailPanels;

    [SerializeField] private List<CharaSequenceUiPanel> seqUi = new();

    public CharaSequenceUiPanel InitCharaSeqUi()
    {
        CharaSequenceUiPanel charaSeqUi = Instantiate(charaSeqUiPrefab, seqUiParent).GetComponent<CharaSequenceUiPanel>();
        seqUi.Add(charaSeqUi);
        return charaSeqUi;
    }

    public void SetHeroTurnPanelState(bool isActive) => arenaHeroTurnPanel.gameObject.SetActive(isActive);
    public void AssignHeroDetailUi(int indx, Character chara) => heroDetailPanels[indx].SetCurrentCharaUi(chara);
    public void AssignEnemyDetailUi(int indx, Character chara) => enemyDetailPanels[indx].SetCurrentCharaUi(chara);
    public void SetButtonSkillDetail(AttackPatternConfig[] configs) => arenaHeroTurnPanel.SetCurrentSkillButtons(configs);
    public void InitActionButtons(UnityAction atk1, UnityAction atk2, UnityAction atk3, UnityAction ult) => arenaHeroTurnPanel.InitActionButtons(atk1, atk2, atk3, ult);
    public void SetActiveButtonIndicator(int idx) => arenaHeroTurnPanel.SetButtonSkilLActiveState(idx);
    public void InitSkipButtons(UnityAction flee, UnityAction skip) => hudPanel.InitSkipButtons(flee, skip);
    public void SetCurrentTurnText(int idx) => hudPanel.SetTurnText(idx);
    public void ActiveWinPanel() => winPanel.SetActive(true);
    public void ActiveLosePanel() => losePanel.SetActive(true);
    public void SetActiveNewTurn(bool state, int currTurn = 0, int maxTurn = 0)
    {
        newTurnPanel.gameObject.SetActive(state);

        if(state)
        {
            newTurnPanel.SetCurrentTurn(currTurn, maxTurn);
        }
    }

    public void ActivateWarningPanel()
    {
        if(warningPanel.gameObject.activeInHierarchy)
            warningPanel.gameObject.SetActive(false);

        warningPanel.gameObject.SetActive(true);
    }

    public void ArrangeSequenceUi(List<Character> charas)
    {
        for (int i = 0; i < seqUi.Count; i++)
        {
            int temp = i;
            if (temp > charas.Count)
            {
                seqUi[temp].gameObject.SetActive(false);
                return;
            }

            seqUi[temp].InitUi(charas[temp]);
        }
    }

    public void SetCharacterSeqUiActive(int idx)
    {
        for (int i = 0; i < seqUi.Count; i++)
        {
            int temp = i;
            seqUi[temp].SetActiveIndicatorState(temp == idx);
        }
    }
}
