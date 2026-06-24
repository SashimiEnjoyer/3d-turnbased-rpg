using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDetailPanel : MonoBehaviour
{
    [SerializeField] private Image currentCharaImg;
    [SerializeField] private TMP_Text currentCharaName;
    [SerializeField] private Slider currentCharaHp;
    [SerializeField] private Slider currentCharaMana;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text manaText;

    private Character chara;

    public void SetCurrentCharaUi(Character chara)
    {
        gameObject.SetActive(true);
        this.chara = chara;
        this.chara.OnUpdateUi += RefreshUi;
        RefreshUi();
    }

    public void RefreshUi()
    {
        currentCharaImg.sprite = chara.GetCharaDetail().charaSprite;
        currentCharaName.text = chara.GetCharaDetail().charaName;
        currentCharaHp.maxValue = chara.GetMaxHp();
        currentCharaHp.value = chara.CurrentHp;
        currentCharaMana.maxValue = chara.GetMaxMana();
        currentCharaMana.value = chara.CurrentMana;

        hpText.SetText($"{chara.CurrentHp}/{chara.GetMaxHp()}");
        manaText.SetText($"{chara.CurrentMana}/{chara.GetMaxMana()}");
    }
}
