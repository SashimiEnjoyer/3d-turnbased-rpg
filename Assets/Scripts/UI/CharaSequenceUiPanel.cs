using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharaSequenceUiPanel : MonoBehaviour
{
    [SerializeField] private Image charaImg;
    [SerializeField] private TMP_Text charaName;
    [SerializeField] private TMP_Text charaSpeed;

    public void InitUi(Character chara)
    {
        charaImg.sprite = chara.GetCharaDetail().charaSprite;
        charaName.text = chara.GetCharaDetail().charaName;
        charaSpeed.text = chara.GetSpeed().ToString();
    }
}
