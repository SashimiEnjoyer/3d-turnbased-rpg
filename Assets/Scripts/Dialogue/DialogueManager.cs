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
    [SerializeField] private SODialogueLines dialogueLines;
    private GameObject dialogueObject;
    private DialoguePanel dialogueUI;
    private int dialogueIndex = 0;

    public bool CanManuallySkip = false;
    public bool isOpen = false;
    public bool ObjectDestroyed = false;
    public bool DialogueEnd = false;
    public UnityEvent onDialogueEnded;

    public void ExecuteInteractable()
    {
        if (!isOpen)
        {
            isOpen = true;

            dialogueObject = Instantiate(dialogueModalUIPrefab);
            dialogueUI = dialogueObject.GetComponentInChildren<DialoguePanel>();
            dialogueUI.SetDialogueUI(dialogueLines.dialogues[0]);

            GameManager.instance.SwitchActionMaps("Dialogue");

            if(CanManuallySkip)
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
        if(CanManuallySkip)
            dialogueUI.OnNextDialogueButtonPressed -= NextDialogue;

        GameManager.instance.SwitchActionMaps("Player");

        onDialogueEnded?.Invoke();
        gameObject.SetActive(false);
        ObjectDestroyed = true;
        DialogueEnd = true;
        Destroy(dialogueObject);
        dialogueIndex = 0;
    }
}
