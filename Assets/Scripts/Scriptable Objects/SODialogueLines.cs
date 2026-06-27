
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct DialogueData
{
    public string currentCharacterName;
    [TextArea(5, 20)] public string characterDialogue;
    public UnityEvent onCurrentDialogueEvent;
};


[CreateAssetMenu(fileName = "New Dialogue", menuName = "Scriptable Objects/Dialogue Line")]
public class SODialogueLines : ScriptableObject
{
    public DialogueData[] dialogues;
}
