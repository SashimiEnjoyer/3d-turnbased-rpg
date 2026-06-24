using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ArenaHeroTurnPanel : MonoBehaviour
{
    [SerializeField] private Button atk1Btn;
    [SerializeField] private Button atk2Btn;
    [SerializeField] private Button atk3Btn;
    [SerializeField] private Button ultBtn;
    [SerializeField] private Button fleeBtn;
    [SerializeField] private Button skipBtn;

    [SerializeField] private Image currentCharaImg;
    [SerializeField] private TMP_Text currentCharaName;
    [SerializeField] private Slider currentCharaHp;
    [SerializeField] private Slider currentCharaMana;

    public void InitActionButtons(UnityAction atk1, UnityAction atk2, UnityAction atk3, UnityAction ult)
    {
        atk1Btn.onClick.AddListener(atk1);
        atk2Btn.onClick.AddListener(atk2);
        atk3Btn.onClick.AddListener(atk3);
        ultBtn.onClick.AddListener(ult);
    }

    public void InitSkipButtons(UnityAction flee, UnityAction skip)
    {
        fleeBtn.onClick.AddListener(flee);
        skipBtn.onClick.AddListener(skip);
    }

    public void SetCurrentCharaUi(Character chara)
    {
        currentCharaImg.sprite = chara.GetCharaDetail().charaSprite;
        currentCharaName.text = chara.GetCharaDetail().charaName;
        currentCharaHp.maxValue = chara.GetMaxHp();
        currentCharaHp.value = chara.CurrentHp;
        currentCharaMana.maxValue = chara.GetMaxMana();
        currentCharaMana.value = chara.CurrentMana;
    }
}
