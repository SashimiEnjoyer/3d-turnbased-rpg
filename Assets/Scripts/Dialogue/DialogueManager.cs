using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct DialogueImages
{
    public Sprite characterImage;
    public float imageSize01;
}

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueModalUIPrefab;
    private GameObject dialogueObject;
    private DialoguePanel dialogueUI;
    public bool isOpen = false;
    private int dialogueIndex = 0;
    public bool ObjectDestroyed = false;
    public bool DialogueEnd = false;
    [SerializeField] private SODialogueLines dialogueLines;
    public UnityEvent onDialogueEnded;

    public void ExecuteInteractable()
    {
        if (!isOpen)
        {
            isOpen = true;

            dialogueObject = Instantiate(dialogueModalUIPrefab);
            dialogueUI = dialogueObject.GetComponentInChildren<DialoguePanel>();
            dialogueUI.SetDialogueUI(dialogueLines.dialogues[0]);
            dialogueUI.OnNextDialogueButtonPressed += NextDialogue;
        }
    }

    public void NextDialogue()
    {
        dialogueIndex += 1;

        if (dialogueIndex < dialogueLines.dialogues.Length)
            dialogueUI.SetDialogueUI(dialogueLines.dialogues[dialogueIndex]);
        else
        {
            EndDialogue();
        }

        dialogueLines.dialogues[dialogueIndex].onCurrentDialogueEvent?.Invoke();
    }

    private void EndDialogue()
    {
        dialogueUI.OnNextDialogueButtonPressed -= NextDialogue;
        onDialogueEnded?.Invoke();
        gameObject.SetActive(false);
        ObjectDestroyed = true;
        DialogueEnd = true;
        Destroy(dialogueObject);
        dialogueIndex = 0;
    }
}
