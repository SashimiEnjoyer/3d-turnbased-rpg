using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour
{
    public UnityAction OnNextDialogueButtonPressed;

    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] Button nextButton;

    public void SetDialogueUI(DialogueData _data)
    {
        nextButton.onClick.RemoveListener(NextDialogue);
        nextButton.onClick.AddListener(NextDialogue);
        nameText.text = _data.currentCharacterName;
        dialogueText.text = _data.characterDialogue;    
    }

    public void NextDialogue()
    {
        OnNextDialogueButtonPressed?.Invoke();
    }
}
