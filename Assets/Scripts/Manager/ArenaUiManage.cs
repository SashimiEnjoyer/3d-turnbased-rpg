
using System.Collections.Generic;
using UnityEngine;

public class ArenaUiManage : MonoBehaviour
{
    [SerializeField] private GameObject charaSeqUiPrefab;
    [SerializeField] private Transform seqUiParent;

    [SerializeField] private List<CharaSequenceUiPanel> seqUi = new();

    public void InitCharaSeqUi(Character chara)
    {
        CharaSequenceUiPanel charaSeqUi = Instantiate(charaSeqUiPrefab, seqUiParent).GetComponent<CharaSequenceUiPanel>();
        charaSeqUi.InitUi(chara);
        seqUi.Add(charaSeqUi);
    }
}
